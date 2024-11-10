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

function darkMode() {
    if (document.getElementById('darkModeSwitch').checked) {
        document.documentElement.setAttribute("data-bs-theme", "dark");
        setCookie("darkMode", "true", 7);
    } else {
        document.documentElement.setAttribute("data-bs-theme", "light");
        setCookie("darkMode", "false", 7);
    }
}

function setCookie(name, value, days) {
    let expires = "";
    if (days) {
        const date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + encodeURIComponent(value) + expires + "; path=/; SameSite=Lax";
}

function getCookie(name) {
    const nameEQ = name + "=";
    const cookies = document.cookie.split(';');
    for (let i = 0; i < cookies.length; i++) {
        let c = cookies[i].trim();
        if (c.indexOf(nameEQ) === 0) return decodeURIComponent(c.substring(nameEQ.length, c.length));
    }
    return null;
}

function checkDarkMode() {
    const cookie = getCookie("darkMode");
    const cookieValue = cookie === "true";
    if (cookieValue) {
        document.documentElement.setAttribute("data-bs-theme", "dark");
        document.getElementById('darkModeSwitch').checked = true;
    } else {
        document.documentElement.setAttribute("data-bs-theme", "light");
    }

    //make body visible once the darkmode is determined
    document.body.style.display = "block";
}
