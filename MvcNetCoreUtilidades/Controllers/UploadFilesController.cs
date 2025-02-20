using Microsoft.AspNetCore.Mvc;
using MvcNetCoreUtilidades.Helpers;

namespace MvcNetCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private HelperPathProvider helperPath;

        public UploadFilesController(HelperPathProvider helperPath)
        {
            this.helperPath = helperPath;
        }
        public  IActionResult SubirFichero()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> SubirFichero(IFormFile fichero)
        {


            string fileName = fichero.FileName.ToLower();
            string path = this.helperPath.MapPath(fileName,Folders.Images);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            //ViewData["PATH"] = this.helperPath.MapUrlPath(fileName,Folders.Images);
            ViewData["PATH"] = this.helperPath.MapUrlPathV2(fileName, Folders.Images);

            return View();

        }
    }
}
