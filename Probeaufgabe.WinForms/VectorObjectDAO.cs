using Newtonsoft.Json;
using Probeaufgabe.WinForms.Models;
using Probeaufgabe.WinForms.Models.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probeaufgabe.WinForms
{
    class VectorObjectDAO
    {
        public static List<IVectorObject> GetVectorObjects(string filePath, InputFileFormat inputFileFormat)
        {
            string fileContent = ReadFile(filePath);

            // mir ist bewusst, das man dynamic nicht umbedingt benutzen sollte
            // ich mache es hier, um eine unnöige datenklasse zu fermeiden, die für neue VectorObject typen erweitert werden müsste. 
            List<dynamic> inputObjects;
            switch (inputFileFormat)
            {
                case InputFileFormat.Json:
                    inputObjects = SerilizeJson(fileContent);
                    break;
                default:
                    throw new NotImplementedException("InputFileFormat nicht Inplementiert");
            }

            List<IVectorObject> returnList = CastToVectorObjects(inputObjects);

            return returnList;
        }

        private static List<IVectorObject> CastToVectorObjects(List<dynamic> inputObjects)
        {
            List<IVectorObject> returnList = new List<IVectorObject>();
            foreach (var item in inputObjects)
            {
                IVectorObject newVectorObject;
                try
                {
                    switch (Helper.VectorObjectHelper.StringToShapes((string)item.type))
                    {
                        case Shapes.Line:
                            newVectorObject = new LineVectorObject(item);
                            break;
                        case Shapes.Triangle:
                            newVectorObject = new TriangleVectorObject(item);
                            break;
                        case Shapes.Circle:
                            newVectorObject = new CircleVectorObject(item);
                            break;
                        default:
                            throw new NotImplementedException($"Form '{item.Type}' wurde nicht implementiert");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Vector Objekt konnte nicht interpretiert werden: {ex.Message}\n\t{item}", ex);
                }

                returnList.Add(newVectorObject);
            }

            return returnList;
        }

        private static List<dynamic> SerilizeJson(string jsonContent)
        {
            List<dynamic> inputObjects;
            try
            {
                inputObjects = JsonConvert.DeserializeObject<List<dynamic>>(jsonContent);
            }
            catch (Exception ex)
            {
                throw new Exception($"Datei konnte nicht Interpretiert werden", ex);
            }

            return inputObjects;
        }

        private static string ReadFile(string filePath)
        {
            string fileContent;
            try
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    fileContent = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Datei konnte nicht eingelesen werden", ex);
            }

            return fileContent;
        }
    }
}
