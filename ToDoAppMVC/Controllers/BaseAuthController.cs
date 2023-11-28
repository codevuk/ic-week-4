using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ToDoAppMVC.Controllers;

public class BaseAuthController : ControllerBase
{
    public BaseAuthController() : base()
    {

    }

    protected int GetUserId()
    {
        var claim = this.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.PrimarySid);

        if (claim == null)
        {
            throw new Exception("Claim not found in token. Please log in again");
        }

        return int.Parse(claim.Value);
    }
}