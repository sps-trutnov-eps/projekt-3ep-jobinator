// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Open the popup form when the button is clicked
document.getElementById('openPopupBtn').onclick = function () {
    document.getElementById('popupForm').style.display = 'block';
};

// Close the popup form when the "X" is clicked
document.getElementById('closePopupBtn').onclick = function () {
    document.getElementById('popupForm').style.display = 'none';
};

// Close the popup form when clicking outside of it
window.onclick = function (event) {
    var popup = document.getElementById('popupForm');
    if (event.target === popup) {
        popup.style.display = 'none';
    }
};

// Handle form submission (you can later integrate this with your backend)
document.getElementById('postForm').onsubmit = function (e) {
    e.preventDefault();
    var title = document.getElementById('postTitle').value;
    var content = document.getElementById('postContent').value;

    // Example of what to do with the form data (you can send it to a server)
    console.log('Post Submitted:', title, content);

    // Close the popup form after submission
    document.getElementById('popupForm').style.display = 'none';

    // Clear form fields
    document.getElementById('postForm').reset();
};

