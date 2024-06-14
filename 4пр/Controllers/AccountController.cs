using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PKS4.DbContexts;
using PKS4.Models;
using PKS4.Models.Data;

public class AccountController : Controller
{
    private readonly ApplicationContext _context;

    private User? AuthEntry = null;

    public AccountController(ApplicationContext context)
    {
        _context = context;
    }

    // GET: /Account/Authorize
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Authenticate user here (compare credentials with database)
            if (IsValidUser(model.Username, model.Password))
            {
                // If authentication succeeds, store username in session and redirect
                return RedirectToAction("Menu", new { username = model.Username });
            }
            else
            {
                // If authentication fails, display error message
                ViewBag.Error = "Invalid username or password";
                return View(model);
            }
        }
        return View(model);
    }

    public IActionResult Register()
    {
        return View();
    }

    // POST: /Account/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(User userModel)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(userModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home"); // Redirect to Home/Index after registration
        }
        return View("Login", userModel); // Return the form with validation errors if model is not valid
    }

    // Dummy method for simulating user authentication
    private bool IsValidUser(string username, string password)
    {
        // Retrieve user from the database based on the provided username
        var user = _context.Users.FirstOrDefault(u => u.Login == username);

        // If user with the provided username exists
        if (user != null)
        {
            // Check if the provided password matches the user's password
            if (user.Password == password)
            {
                return true; // Authentication successful
            }
        }
        return false; // Authentication failed
    }

    #region Menu (Authorized)

    [HttpPost]
    public IActionResult SendMessage(string senderLogin, string recipientLogin, string subject, string messageText)
    {
        // Check if the current user is authenticated
        if (string.IsNullOrEmpty(senderLogin))
        {
            // Redirect to login page or handle unauthorized access
            return RedirectToAction("Login", "Account");
        }

        // Retrieve the recipient user from the database
        var recipient = _context.Users.FirstOrDefault(u => u.Login == recipientLogin);
        var sender = _context.Users.FirstOrDefault(u => u.Login == senderLogin);

        if(sender == null)
        {
            return RedirectToAction("Error");
        }

        if (recipient != null)
        {
            // Create a new message
            var message = new Message
            {
                SenderId = sender!.Id,
                Sender = sender,
                ReceiverId = recipient.Id,
                Receiver = recipient,
                Header = subject,
                MessageText = messageText,
                SendingDate = DateTime.UtcNow,
                Status = 0 // Assuming 0 means the message is not read yet
            };

            // Save the message to the database
            _context.Messages.Add(message);
            _context.SaveChanges();

            return RedirectToAction("Menu", new { username = senderLogin });
        }
        else
        {
            ViewBag.Error = "Recipient not found";
            // Reload the Menu view with the error message
            return RedirectToAction("Menu", new { username = senderLogin });
        }
    }
    #endregion

    public IActionResult Menu(string username, string senderLogin, DateTime? fromDate, DateTime? toDate, int? status, string sortOrder)
    {
        // Retrieve the user from the database based on the provided username
        var user = _context.Users.FirstOrDefault(u => u.Login == username);

        if (user != null)
        {
            // Query messages for the user
            var query = _context.Messages.Where(m => m.ReceiverId == user.Id);

            // Apply filters based on user input
            if (!string.IsNullOrEmpty(senderLogin))
            {
                query = query.Where(m => m.Sender.Login == senderLogin);
            }

            if (fromDate.HasValue)
            {
                query = query.Where(m => m.SendingDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(m => m.SendingDate <= toDate.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(m => m.Status == status.Value);
            }

            // Sort messages based on the sort order
            switch (sortOrder)
            {
                case "date":
                    query = query.OrderBy(m => m.SendingDate);
                    break;
                    // Add other sorting options if needed
            }

            // Retrieve filtered and sorted messages
            var messages = query.ToList();

            // Populate filter criteria
            var filterCriteria = new MessageFilterCriteria
            {
                SenderLogin = senderLogin,
                FromDate = fromDate,
                ToDate = toDate,
                Status = status
            };

            // Pass the user, filtered/sorted messages, and filter criteria to the Menu view
            return View("Menu", new MenuViewModel { User = user, Messages = messages, FilterCriteria = filterCriteria });
        }

        // If user not found, redirect to login page
        return RedirectToAction("Login");
    }
}
