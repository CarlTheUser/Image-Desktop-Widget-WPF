using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Desktop_Widget.Model
{
    public abstract class Model : INotifyPropertyChanged
    {

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler Saved;

        public event EventHandler EditBegan;

        public event EventHandler EditCancelled;

        public event EventHandler EditApplied;

        public event EventHandler Deleted;

        public event EventHandler<ErrorOccuredEventArgs> ErrorOccured;

        #endregion

        #region Private Properties

        private IDictionary<string, object> _propertyBackups = null;

        protected IDictionary<string, object> PropertyBackups
        {
            get
            {
                return _propertyBackups = _propertyBackups ?? new Dictionary<string, object>();
            }
        }

        #endregion

        #region PublicProperties

        private ModelState state = ModelState.New;  //initializes as new

        public ModelState State
        {
            get => state;
            protected set
            {
                if (state == value) return;
                state = value;
                OnPropertyChanged("State");
            }
        }

        #endregion

        #region AbstractBehaviors

        protected abstract void SaveMethod();

        protected abstract void EditMethod();

        protected abstract void DeleteMethod();

        protected abstract void BackupProperties();

        protected abstract void RestoreProperties();

        protected abstract void ClearPropertyBackups();

        #endregion
        
        #region PublicBehaviors

        public void Save()
        {
            try
            {
                if(State == ModelState.New)
                {
                    SaveMethod();
                    State = ModelState.Old;
                    OnSaved();
                }
                else if(State == ModelState.Editing || State == ModelState.Old)
                {
                    EditMethod();
                    OnEditApplied();
                    ClearPropertyBackups();
                    State = ModelState.Old;
                }
            }
            catch (Exception e)
            {
                State = ModelState.Dirty;
                OnErrorOccured(e.Message);
            }
        }

        public void BeginEdit()
        {
            BackupProperties();
            State = ModelState.Editing;
            OnEditBegan();
        }

        public void CancelEdit()
        {
            OnEditCancelled();
            RestoreProperties();
            ClearPropertyBackups();
            State = ModelState.Old;
        }

        //public void ApplyEdit()
        //{
        //    try
        //    {
        //        EditMethod();
        //        OnEditApplied();
        //        ClearPropertyBackups();
        //        State = ModelState.Old;
                
        //    }
        //    catch (Exception e)
        //    {
        //        State = ModelState.Dirty;
        //        OnErrorOccured(e.Message);
        //    }
            
        //}

        public void Delete()
        {
            try
            {
                DeleteMethod();
                State = ModelState.Deleted;
                OnDeleted();
            }
            catch (Exception e)
            {
                State = ModelState.Dirty;
                OnErrorOccured(e.Message);
            }
        }

        #endregion

        #region EventTriggers

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnSaved()
        {
            Saved?.Invoke(this, EventArgs.Empty);
        }

        private void OnEditBegan()
        {
            EditBegan?.Invoke(this, EventArgs.Empty);
        }

        private void OnEditCancelled()
        {
            EditCancelled?.Invoke(this, EventArgs.Empty);
        }

        private void OnEditApplied()
        {
            EditApplied?.Invoke(this, EventArgs.Empty);
        }

        private void OnDeleted()
        {
            Deleted?.Invoke(this, EventArgs.Empty);
        }

        protected void OnErrorOccured(string message)
        {
            ErrorOccured?.Invoke(this, new ErrorOccuredEventArgs(message));
        }

        #endregion

        public class ErrorOccuredEventArgs : EventArgs
        {
            public string Message { get; private set; }

            public ErrorOccuredEventArgs(string message)
            {
                Message = message;
            }
        }

        public enum ModelState
        {
            New = 0,
            Editing = 1,
            Old = 2 ,
            Deleted = 3,
            Dirty = 4
        }

    }
}
