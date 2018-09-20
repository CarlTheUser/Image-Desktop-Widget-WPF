using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Model;
using DataLayer.ModelService;
using DataLayer.ModelService.XMLServices;
using DataLayer.Client.Data;
using System.IO;
using Logging;

namespace DataLayer.Client.Service
{
    public sealed class FramedImageOperation
    {
        //This class and any class from the DataLayer.Client namespace are 
        //the visible implementations of the data layer
        //to the clients. all classes outside this  namespace must be internal
        //probably seal the classes on this layer as well
        //to prevent further inheritance from the client

        //Path to the directory used to store images after copying from original location.
        private static readonly string ImageDirectoryPath = Configuration.Instance.UserImageDirectoryPath;
        
        //The object that will handle all internal implementations
        private FramedImageModelPersistence framedImagedPersistence;

        private readonly ExceptionLogger logger;

        #region CustomExceptionMessages

        //Throw errors on client side with custom messages after logging the error
        //The original error should be hidden from the client, but logged in a text. Hence we show a general error message

        private static readonly Exception FILE_NOT_IMAGE_ERROR = new Exception("File received is not an image.");
        private static readonly Exception SAVE_ERROR = new Exception("An error occured while saving image data.");
        private static readonly Exception EDIT_ERROR = new Exception("An error occured when saving changes to the image data.");
        private static readonly Exception DELETE_ERROR = new Exception("An error occured while deleting image data.");
        private static readonly Exception READ_ERROR = new Exception("An error occured while reading the image data.");

        #endregion

        #region Constructor

        public FramedImageOperation()
        {
            //from here you can change the implementation of your persistence service
            //without breaking the app by just extending the FramedImageModelPersistence interface
            //framedImagedService => new XMLService(), SQLService, WebService (lol), EtcServie
            framedImagedPersistence = new XMLFramedImageModelPersistence();

            logger = new ExceptionLogger(TextLogger.GetInstance(Configuration.Instance.TextLogsPath));
        }

        #endregion

        #region PublicBehaviors

        /// <summary>
        /// Saves a FramedImageDTO to a persistent storage
        /// </summary>
        /// <param name="framedImage"></param>
        /// <exception cref="Exception">Will throw if errors occured while saving the FramedImageDTO to a persistent storage.</exception>
        /// <exception cref="Exception">Will throw if received file is not an image.</exception>
        public void Save(FramedImageDTO framedImage)
        {
            if (framedImage != null)
            {
                try
                {
                    FileStream fileStream = new FileStream(framedImage.ImageFilepath, FileMode.Open, FileAccess.Read);
                    if (fileStream.IsImage())
                    {
                        string ext = framedImage.ImageFilepath.Substring(framedImage.ImageFilepath.LastIndexOf(".") + 1);

                        int imageNumber = framedImagedPersistence.GetCount();

                        string datetime = DateTime.Now.Ticks.ToString();

                        string newFilename = String.Format("Image-{0}-{1}.{2}", imageNumber++, datetime, ext);

                        FramedImageModel model = new FramedImageModel
                        {
                            Id = framedImage.Id,
                            Filename = newFilename,
                            Witdh = framedImage.Witdh,
                            Height = framedImage.Height,
                            LocationX = framedImage.LocationX,
                            LocationY = framedImage.LocationY,
                            FrameThickness = framedImage.FrameThickness,
                            RotateAngle = framedImage.RotateAngle,
                            Caption = framedImage.Caption,
                            EnableCaption = framedImage.EnableCaption,
                            EnableShadow = framedImage.EnableShadow,
                            ShadowOpacity = framedImage.ShadowOpacity,
                            ShadowDepth = framedImage.ShadowDepth,
                            ShadowDirection = framedImage.ShadowDirection,
                            ShadowBlurRadius = framedImage.ShadowBlurRadius
                        };

                        framedImagedPersistence.Save(model);

                        framedImage.Id = model.Id;
                        
                        CopyImage(framedImage.ImageFilepath, newFilename);

                        framedImage.ImageFilepath = ImageDirectoryPath + "\\" + model.Filename;

                        fileStream.Close();
                        fileStream.Dispose();

                    }
                    else throw FILE_NOT_IMAGE_ERROR;
                }
                catch (Exception ex)
                {
                    logger.LogException(ex);
                    throw SAVE_ERROR;
                }
                
            }
        }

        /// <summary>
        /// Saves changes made to a FramedImageDTO back to the persistent storage.
        /// </summary>
        /// <param name="framedImage"></param>
        /// <exception cref="Exception">Will throw if errors occured while updating the FramedImageDTO back to a persistent storage.</exception>
        public void Edit(FramedImageDTO framedImage)
        {
            if (framedImage != null)
            {
                try
                {
                    string filename = Path.GetFileName(framedImage.ImageFilepath);

                    framedImagedPersistence.Edit(
                        framedImage.Id,
                        new FramedImageModel
                        {
                            Id = framedImage.Id,
                            Filename = filename,
                            Witdh = framedImage.Witdh,
                            Height = framedImage.Height,
                            LocationX = framedImage.LocationX,
                            LocationY = framedImage.LocationY,
                            FrameThickness = framedImage.FrameThickness,
                            RotateAngle = framedImage.RotateAngle,
                            Caption = framedImage.Caption,
                            EnableCaption = framedImage.EnableCaption,
                            EnableShadow = framedImage.EnableShadow,
                            ShadowOpacity = framedImage.ShadowOpacity,
                            ShadowDepth = framedImage.ShadowDepth,
                            ShadowDirection = framedImage.ShadowDirection,
                            ShadowBlurRadius = framedImage.ShadowBlurRadius
                        });
                }
                catch(Exception ex)
                {
                    logger.LogException(ex);
                    throw EDIT_ERROR;
                }
            }
        }

        /// <summary>
        /// Permanently removes a FramedImageDTO from the persistent storage.
        /// </summary>
        /// <param name="framedImage"></param>
        /// <exception cref="Exception">Will throw if errors occured while removing FramedImageDTO from persistent storage.</exception>
        public void Delete(FramedImageDTO framedImage)
        {
            try
            {
                if (framedImage != null)
                {
                    framedImagedPersistence.Delete(framedImage.Id);

                    DeleteImage(framedImage.ImageFilepath);
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                throw DELETE_ERROR;
            }
        }

        /// <summary>
        /// Returns a FramedImageDTO that matches the given id parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>FramedImageDTO</returns>
        /// <exception cref="Exception">Will throw if errors occured while reading from persistent storage.</exception>
        public FramedImageDTO GetById(int id)
        {
            try
            {
                FramedImageModel model = framedImagedPersistence.GetById(id);

                return model == null ? null : new FramedImageDTO
                {
                    Id = model.Id,
                    ImageFilepath = ImageDirectoryPath + "\\" + model.Filename,
                    Witdh = model.Witdh,
                    Height = model.Height,
                    LocationX = model.LocationX,
                    LocationY = model.LocationY,
                    FrameThickness = model.FrameThickness,
                    RotateAngle = model.RotateAngle,
                    Caption = model.Caption,
                    EnableCaption = model.EnableCaption
                };
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                throw READ_ERROR;
            }
        }

        /// <summary>
        /// Returns all FramedImageDTO from the persistent storage.
        /// </summary>
        /// <returns>Collection of all FramedImageDTO.</returns>
        /// <exception cref="Exception">Will throw if errors occured while reading from persistent storage.</exception>
        public IEnumerable<FramedImageDTO> GetAll()
        {
            List<FramedImageDTO> returnValue = new List<FramedImageDTO>();

            try
            {

                IEnumerable<FramedImageModel> models = framedImagedPersistence.GetAll();

                returnValue = (from model in models
                               select new FramedImageDTO
                               {
                                   Id = model.Id,
                                   ImageFilepath = ImageDirectoryPath + "\\" + model.Filename,
                                   Witdh = model.Witdh,
                                   Height = model.Height,
                                   LocationX = model.LocationX,
                                   LocationY = model.LocationY,
                                   FrameThickness = model.FrameThickness,
                                   RotateAngle = model.RotateAngle,
                                   Caption = model.Caption,
                                   EnableCaption = model.EnableCaption,
                                   EnableShadow = model.EnableShadow,
                                   ShadowOpacity = model.ShadowOpacity,
                                   ShadowDepth = model.ShadowDepth,
                                   ShadowDirection = model.ShadowDirection,
                                   ShadowBlurRadius = model.ShadowBlurRadius
                               }).ToList();
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                returnValue = null;
                throw READ_ERROR;
            }
            return returnValue;
        }

        #endregion

        private void CopyImage(string imagePathSource, string newName)
        {
            string currentfilename = Path.GetFileName(imagePathSource);

            string newPath = ImageDirectoryPath + "\\" + newName;

            try
            {
                File.Copy(imagePathSource, newPath, true);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                throw;
            }
        }

        private void DeleteImage(string imagePath)
        {
            try
            {
                File.Delete(imagePath);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                throw;
            }
            
        }

        //public class FramedImageOperationEventArgs : EventArgs
        //{
        //    public FramedImageDTO FramedImageData { get; set; }
        //    public FramedImageOperationEventArgs(FramedImageDTO framedImageData) { FramedImageData = framedImageData; }
        //}

        //public class ErrorEventArgs : EventArgs
        //{
        //    public string Message { get; private set; }
        //    public ErrorEventArgs(string message) { Message = message; }
        //}

    }
}
