using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LibraryDAL;
using LibraryServiceWebAPI.Models;

namespace LibraryServiceWebAPI.Controllers
{
    public class BooksController : ApiController
    {
        [HttpGet]
        [Route("api/books")]
        public HttpResponseMessage GetAllBooks()
        {
            try
            {
                using (BooksLibraryEntities entities = new BooksLibraryEntities())
                {
                    IEnumerable<Book> BooksList = entities.Books.ToList();
                    if (BooksList != null && BooksList.Count() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, BooksList);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Books Found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/books/{id:int}")]
        public HttpResponseMessage GetBookByID(int id)
        {
            try
            {
                using (BooksLibraryEntities entities = new BooksLibraryEntities())
                {
                    Book entity = entities.Books.Where(b => b.Id == id).FirstOrDefault();
                    if (entity != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Book with ID - " + id + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/books/{title:alpha}")]
        public HttpResponseMessage GetBookByTitle(string title)
        {
            try
            {
                using (BooksLibraryEntities entities = new BooksLibraryEntities())
                {
                    IEnumerable<Book> books = entities.Books.Where(b => b.Title.Contains(title)).ToArray();
                    if (books != null && books.Count() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, books);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Book with title - " + title + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/books")]
        public HttpResponseMessage AddBook([FromBody] Book book)
        {
            try
            {
                using (BooksLibraryEntities entities = new BooksLibraryEntities())
                {
                    entities.Books.Add(book);
                    entities.SaveChanges();
                    BookDTO bookDTO = entities.Books.Select(b => new BookDTO
                                    {
                                        Id = b.Id,
                                        Title = b.Title,
                                        Author = b.Author,
                                        Genre = b.Genre,
                                        ISBN = b.ISBN,
                                        PublishDate = b.PublishDate,
                                        Publisher = b.Publisher
                                    }).FirstOrDefault();
                    HttpResponseMessage responseMessage = 
                        Request.CreateResponse(HttpStatusCode.Created);
                    responseMessage.Headers.Location = 
                        new Uri(Request.RequestUri + bookDTO.Id.ToString());
                    return responseMessage;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [Route("api/books/{id:int}")]
        public HttpResponseMessage UpdateBookbyID(int id, [FromBody] BookDTO bookDetails)
        {
            try
            {
                using (BooksLibraryEntities entities = new BooksLibraryEntities())
                {
                    Book entity = entities.Books.Where(b => b.Id == id).FirstOrDefault();
                    if (entity != null)
                    {
                        if (!string.IsNullOrEmpty(bookDetails.Title)) { entity.Title = bookDetails.Title; }
                        if (!string.IsNullOrEmpty(bookDetails.ISBN)) { entity.ISBN = bookDetails.ISBN; }
                        if (!string.IsNullOrEmpty(bookDetails.Author)) { entity.Author = bookDetails.Author; }
                        if (!string.IsNullOrEmpty(bookDetails.Genre)) { entity.Genre = bookDetails.Genre; }
                        if (!string.IsNullOrEmpty(bookDetails.Publisher)) { entity.Publisher = bookDetails.Publisher; }
                        if (bookDetails.PublishDate != null) { entity.PublishDate = bookDetails.PublishDate; }
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Book with ID - " + id + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        [Route("api/books/{id:int}")]
        public HttpResponseMessage RemoveBook(int id)
        {
            try
            {
                using (BooksLibraryEntities entities = new BooksLibraryEntities())
                {
                    Book entity = entities.Books.Where(b => b.Id == id).FirstOrDefault();
                    if (entity != null)
                    {
                        entities.Books.Remove(entity);
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Book with ID - " + id + " not found");
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
