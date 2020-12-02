﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using myStore.myStoreServices;
using myStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myStore.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index(string userID, string status, int? pageNo)
        {
            OrderViewModel model = new OrderViewModel();
            model.UserID = userID;
            model.Status = status;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            var pageSize = ConfigurationsService.Instance.PageSize();

            model.Orders = OrderService.Instance.SearchOrders(userID, status, pageNo.Value, pageSize);
            var totalRecords = OrderService.Instance.SearchOrdersCount(userID, status);

            model.Pager = new Pager(totalRecords, pageNo, pageSize);

            return View(model);
        }

        public ActionResult Details(int ID)
        {
            OrderDetailsViewModel model = new OrderDetailsViewModel();
            model.Order = OrderService.Instance.GetOrderByID(ID);
            if(model.Order != null)
            
            {
                model.OrderBy = UserManager.FindById(model.Order.UserID);
            }

            model.AvailableStatus = new List<string>() { "Pending", "In Progress", "Delivred" };
            

            return View(model);
        }

        public JsonResult ChangeStatus(string status, int ID)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            result.Data = new { Success = OrderService.Instance.UpdateOrderStatus(ID, status) };
            return result;

           
        }
    }
}