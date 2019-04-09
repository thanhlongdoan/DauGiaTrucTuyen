using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DauGiaTrucTuyen.Common
{
    public class Upload
    {
        public string UploadImg(HttpPostedFileBase file)
        {
            string fileName = "";
            if (file != null && file.ContentLength > 0)
            {
                fileName = Path.GetFileName(file.FileName);
                string pathFileName = Guid.NewGuid().ToString() + fileName;
                var path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(@"/Content/Admin/img-product"), pathFileName);
                file.SaveAs(path);
                return "/Content/Admin/img-product/" + pathFileName;
            }
            else
            {
                return null;
            }
        }
    }
}