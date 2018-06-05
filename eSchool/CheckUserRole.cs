//using Microsoft.AspNetCore.Authorization;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace eSchool
//{
//    public class CheckUserRole : AuthorizationHandler<CheckUserRole>, IAuthorizationRequirement
//    {
//        public override void Handle(AuthorizationHandlerContext context, CheckUserRole requirement)
//        {
//            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role))
//            {
//                context.Fail();
//                return;
//            }

//            var role = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;

//            if (role == "Teacher" || role == "Admin")
//            {
//                context.Succeed(requirement);
//            }
//            else
//            {
//                context.Fail();
//            }
//        }

//        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CheckUserRole requirement)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}