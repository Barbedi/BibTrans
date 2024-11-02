﻿const books = document.querySelectorAll(".book-info");
const icons = document.querySelectorAll(".book-details .icon");

books.forEach((book) => {
    book.addEventListener("click", (event) => {
        const bookId = parseInt(event.currentTarget.dataset.bookid);
        const details = document.querySelector(`.book-info[data-bookid="${bookId}"]+.book-details`);
        details.classList.toggle("hidden");
        
        $.ajax({
            url: "Books/SetChosenId",
            method: "POST",
            data: `id=${bookId}`,
            success: () => {
                console.log("Ustawiono id książki na ", bookId);
            },
            error: (error) => {
                console.error("Wystąpił błąd: ", error.statusCode);
            }
        });
    });
});

icons.forEach((icon) => {
    icon.addEventListener("click", (event) => {
        const bookId = event.currentTarget.dataset.bookid;
        const details = document.querySelector(`.book-info[data-bookid="${bookId}"]+.book-details`);
        details.classList.add("hidden");
    });
});
