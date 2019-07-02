using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace DBLib
{
    /// <summary>
    /// Include saving and removing pictures on server
    /// Used for work with books titles
    /// </summary>
    public static class PictureOperations
    {
        public static bool SaveTitlePic(HttpPostedFileBase titlePic, Guid Id, string ServerPicturesPath)
        {
            if (titlePic != null && titlePic.ContentLength > 0)
            {
                try
                {
                    if (titlePic.ContentType != "image/jpeg")
                    {
                        return false;
                    }
                    string path = Path.Combine(ServerPicturesPath, Id.ToString() + ".jpg");

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    titlePic.SaveAs(path);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public static void RemoveTitlePic(Guid Id, string ServerPicturesPath)
        {
            string path = Path.Combine(ServerPicturesPath, Id.ToString() + ".jpg");

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}