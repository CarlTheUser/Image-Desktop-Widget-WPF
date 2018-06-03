using GeneralMerchandise.CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Model
{
    internal class UserModel : PersistibleModel<int>
    {

        private static readonly int DEFAULT_IDENTITY = 0;

        public static UserModel New(string imageFilename, string firstname, string middlename, string lastname, Gender gender, DateTime birthdate, string contactNumber, string email, string address)
        {
            return new UserModel
            {
                Identity = DEFAULT_IDENTITY,
                ImageFilename = imageFilename,
                Firstname = firstname,
                Middlename = middlename,
                Lastname = lastname,
                Gender = gender,
                BirthDate = birthdate,
                ContactNumber = contactNumber,
                Email = email,
                Address = address
            };
        }

        private UserModel() { }

        public override int Identity { get; set; }

        public string ImageFilename { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
