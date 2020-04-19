using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRS.Models;
using PagedList;

namespace PRS.Controllers
{
    [AuthorizeRoles(Role.SuperAdmin, Role.Admin)]
    public class AuditLogController : PRSBaseController
    {
        public ActionResult Index(int? page = 1)
        {
            page = page ?? 1;
            return View(db.AuditLogs.OrderByDescending(o => o.OperationDate).ToPagedList(page.Value, 100));
        }
    }
}