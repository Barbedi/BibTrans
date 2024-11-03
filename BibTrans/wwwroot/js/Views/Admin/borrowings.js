const searchInput = document.querySelector(".search-input input");
const searchLabel = document.querySelector(".search-input label");

searchLabel.addEventListener("click", () => {
    searchInput.focus();
});

searchInput.addEventListener("input", (ev) => {
    const searchValueLength = ev.currentTarget.value.length;
    if (searchValueLength > 0) {
        searchLabel.classList.add("hidden");
    } else {
        searchLabel.classList.remove("hidden");
    }
});