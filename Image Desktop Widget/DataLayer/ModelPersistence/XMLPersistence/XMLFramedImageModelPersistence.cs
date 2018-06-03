using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Model;
using System.Xml.Linq;
using System.IO;
using System.Text;

namespace DataLayer.ModelService.XMLServices
{
    internal sealed class XMLFramedImageModelPersistence : FramedImageModelPersistence
    {
        // XML implementation as data provider for FramedImageModelService

        private static readonly string XML_FILENAME = "UserImageData.xml";

        private static readonly string MODEL_ELEMENT_NAME = "FramedImage";

        private static readonly string MODEL_ELEMENT_COLLECTION_NAME = "UserImages";

        private static readonly string XML_DOCUMENT_LOCATION = Configuration.Instance.AppDataFolderPath + "\\" + XML_FILENAME;
        
        public XMLFramedImageModelPersistence()
        {
            XDocument document;

            if (!File.Exists(XML_DOCUMENT_LOCATION))
            {
                document = new XDocument(new XElement(MODEL_ELEMENT_COLLECTION_NAME));
                document.Save(XML_DOCUMENT_LOCATION);
            }

        }

        public override int GetCount()
        {

            int returnValue = 0;

            XDocument document = XDocument.Load(XML_DOCUMENT_LOCATION);

            returnValue = (from userImage in document
                           .Element(MODEL_ELEMENT_COLLECTION_NAME)
                           .Descendants(MODEL_ELEMENT_NAME)
                           select userImage).Count();

            return returnValue;
        }

        private int GenerateNextId()
        {
            int returnValue = 0;

            XDocument document = XDocument.Load(XML_DOCUMENT_LOCATION);

            try
            {
                returnValue = (from userImage in document
                               .Element(MODEL_ELEMENT_COLLECTION_NAME)
                               .Descendants(MODEL_ELEMENT_NAME)
                               select userImage).Max(x => (int)x.Attribute("Id")) + 1;
            }
            catch
            {
                returnValue = 1;
            }

            return returnValue;
        }

        public override int Save(FramedImageModel model)
        {
            if (model == null) throw new ArgumentNullException("model");

            int id = 0;
            
            try
            {

                XDocument document = XDocument.Load(XML_DOCUMENT_LOCATION);

                XElement UserImages = document.Element(MODEL_ELEMENT_COLLECTION_NAME);

                id = GenerateNextId();

                XElement element = new XElement
                (
                    MODEL_ELEMENT_NAME,
                    new XAttribute("Id", model.Id = id),
                    new XElement("Filename", model.Filename),
                    new XAttribute("Width", model.Witdh),
                    new XAttribute("Height", model.Height),
                    new XAttribute("LocationX", model.LocationX),
                    new XAttribute("LocationY", model.LocationY),
                    new XAttribute("FrameThickness", model.FrameThickness),
                    new XAttribute("RotateAngle", model.RotateAngle),
                    new XElement("Caption", model.Caption, new XAttribute("Enabled", model.EnableCaption), new XAttribute("Text", model.Caption))
                );

                UserImages.AddFirst(element);
                document.Save(XML_DOCUMENT_LOCATION);
            }
            catch
            {
                id = 0;

                throw;
            }

            return id;
        }

        public override void Edit(int id, FramedImageModel updatedModel)
        {
            if (updatedModel == null) throw new ArgumentNullException("updatedModel");

            XDocument document = XDocument.Load(XML_DOCUMENT_LOCATION);

            XElement UserImages = document.Element(MODEL_ELEMENT_COLLECTION_NAME);

            XElement userImage = (from image in UserImages.Elements()
                                  where image.Attribute("Id").Value == id.ToString()
                                  select image).FirstOrDefault();

            userImage.SetElementValue("Filename", updatedModel.Filename);
            userImage.SetAttributeValue("Width", updatedModel.Witdh);
            userImage.SetAttributeValue("Height", updatedModel.Height);
            userImage.SetAttributeValue("LocationX", updatedModel.LocationX);
            userImage.SetAttributeValue("LocationY", updatedModel.LocationY);
            userImage.SetAttributeValue("FrameThickness", updatedModel.FrameThickness);
            userImage.SetAttributeValue("RotateAngle", updatedModel.RotateAngle);
            userImage.Element("Caption").SetAttributeValue("Enabled", updatedModel.EnableCaption);
            userImage.Element("Caption").SetAttributeValue("Text", updatedModel.Caption);

            document.Save(XML_DOCUMENT_LOCATION);
            
        }

        public override void Delete(int id)
        {
            XDocument document = XDocument.Load(XML_DOCUMENT_LOCATION);

            document.Element(MODEL_ELEMENT_COLLECTION_NAME)
                .Elements(MODEL_ELEMENT_NAME)
                .Where(x => x.Attribute("Id").Value == id.ToString())
                .Remove();

            document.Save(XML_DOCUMENT_LOCATION);

        }

        public override FramedImageModel GetById(int id)
        {
            FramedImageModel returnValue = null;

            XDocument document = XDocument.Load(XML_DOCUMENT_LOCATION);

            XElement UserImages = document.Element(MODEL_ELEMENT_COLLECTION_NAME);

            XElement userImage = (from image in UserImages.Elements()
                                  where image.Attribute("Id").Value == id.ToString()
                                  select image).FirstOrDefault();

            if (userImage != null)
            {
                returnValue = new FramedImageModel();

                returnValue.Id = int.Parse(userImage.Attribute("Id").Value);
                returnValue.Filename = userImage.Element("Filename").Value;
                returnValue.Witdh = (double)userImage.Attribute("Width");
                returnValue.Height = (double)userImage.Attribute("Height");
                returnValue.LocationX = (double)userImage.Attribute("LocationX");
                returnValue.LocationY = (double)userImage.Attribute("LocationY");
                returnValue.FrameThickness = (int)userImage.Attribute("FrameThickness");
                returnValue.RotateAngle = int.Parse(userImage.Attribute("RotateAngle").Value);
                returnValue.Caption = userImage.Element("Caption").Attribute("Text").Value != null ? userImage.Element("Caption").Attribute("Text").Value : "";
                returnValue.EnableCaption = (bool)userImage.Element("Caption").Attribute("Enabled");
            }

            return returnValue;
        }

        public override IEnumerable<FramedImageModel> GetAll()
        {
            List<FramedImageModel> userImagesList = new List<FramedImageModel>();

            XDocument document = XDocument.Load(XML_DOCUMENT_LOCATION);

            XElement UserImages = document.Element(MODEL_ELEMENT_COLLECTION_NAME);

            userImagesList = (from image in UserImages.Elements(MODEL_ELEMENT_NAME)

                              let id = (int)image.Attribute("Id")
                              let filename = image.Element("Filename").Value
                              let width = (double)image.Attribute("Width")
                              let height = (double)image.Attribute("Height")
                              let locationX = (double)image.Attribute("LocationX")
                              let locationY = (double)image.Attribute("LocationY")
                              let frameThickness = (int)image.Attribute("FrameThickness")
                              let rotateAngle = (int)image.Attribute("RotateAngle")
                              let captionElement = image.Element("Caption")
                              let captionText = captionElement.Attribute("Text") != null ? captionElement.Attribute("Text").Value : ""
                              let captionEnabled = (bool)captionElement.Attribute("Enabled")

                              select new FramedImageModel
                              {
                                  Id = id,
                                  Filename = filename,
                                  Witdh = width,
                                  Height = height,
                                  LocationX = locationX,
                                  LocationY = locationY,
                                  FrameThickness = frameThickness,
                                  RotateAngle = rotateAngle,
                                  Caption = captionText,
                                  EnableCaption = captionEnabled
                              }).ToList();

            return userImagesList;
        }
                
    }
}
