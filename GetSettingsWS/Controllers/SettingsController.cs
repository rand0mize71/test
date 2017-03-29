using GetSettingsWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace GetSettingsWS.Controllers
{
    public class SettingsController : Controller
    {
        private List<Setting> settings = new List<Setting>
        {
            new Setting {key = "a1 a2", value = "aa" },
            new Setting {key = "a1 b2", value = "ab" },
            new Setting {key = "b1 a2", value = "ba" },
            new Setting {key = "b1 b2", value = "bb" },
            new Setting {key = "a1 b2 c3", value = "abc" },
            new Setting {key = "c1 c2", value = "cc" },
            new Setting {key = "c1 b2", value = "cb" },
            new Setting {key = "a1 c2", value = "ac" }
        };
        
        public ActionResult Index()
        {
            return View();
        }

        public IEnumerable<Setting> GetSettings()
        {
            return settings;
        }

        public JsonResult GetSetting(string searchField)
        {
            if (string.IsNullOrEmpty(searchField))
            {
                return Json(settings, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var setting = settings.Where(s => s.key.Split(' ').Any(x => searchField.Split(' ').Contains(x)));

                return Json(setting, JsonRequestBehavior.AllowGet);
            }
        }

    }
}