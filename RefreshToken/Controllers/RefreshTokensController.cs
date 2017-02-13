using System.Threading.Tasks;
using System.Web.Http;
using RefreshToken.Models;


namespace RefreshToken.Controllers
{
    [RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : ApiController
    {
        public readonly AuthRepository Repo;

        public RefreshTokensController()
        {
            Repo = new AuthRepository();
        }

        [Authorize(Users = "Admin")]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(Repo.GetAllRefreshTokens());
        }
        
        [AllowAnonymous]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var result = await Repo.RemoveRefreshToken(tokenId);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Token Id does not exist");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Repo.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}