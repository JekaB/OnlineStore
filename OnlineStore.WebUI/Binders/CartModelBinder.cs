using System;
using System.Web.Mvc;
using OnlineStore.Domain.Entities;

namespace OnlineStore.WebUI.Binders
{
    public class CartModelBinder: IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // get the Cart from session
            Cart cart = (Cart)controllerContext.HttpContext.Session[sessionKey];

            // create the Cart if there wasn`t one in the session data
            if (cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[sessionKey] = cart;
            }

            //return the cart
            return cart;

        }
    }
}