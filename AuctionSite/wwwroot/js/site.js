// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//Function delete using Ajax just beacuse i wanted to try Type Delete then i reload page beacuse if there would be an error it mostlikley is beacuse it got the deleted by someone else before you refreshed the page.
//This function could be skiped and use httpPost or get then redirect to index page by controller instead of window reload.
function Delete(url) {
    $.ajax({
        url: url,
        type: 'DELETE',
        success: function () {
            window.location.reload();
        }
    });
}