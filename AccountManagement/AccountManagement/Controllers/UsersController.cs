using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClassLibrary1;

namespace AccountManagement.Controllers
{
    public class UsersController : ApiController
    {

        public IEnumerable<User> Get()
        {
            using (luckyEntities entities = new luckyEntities())
            {
                return entities.Users.ToList();
            }
        }
        public User Get(int id)
        {
            using (luckyEntities entities = new luckyEntities())
            {
                return entities.Users.FirstOrDefault(e => e.ID == id);
            }
        }
        public HttpResponseMessage Post([FromBody] User user)
        {
            try
            {
                using (luckyEntities entities = new luckyEntities())
                {
                    entities.Users.Add(user);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, user);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        user.ID.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (luckyEntities entities = new luckyEntities())
                {
                    var entity = entities.Users.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.Users.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]User employee)
        {
            try
            {
                using (luckyEntities entities = new luckyEntities())
                {
                    var entity = entities.Users.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Email = employee.Email;
                        entity.MobileNumber = employee.MobileNumber;
                        entity.DateOfBirth = employee.DateOfBirth;
                        entity.Modified = employee.Modified;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
