const functionalitiesList = document.querySelectorAll(".functionalities-list li");
const functionalityDescriptions = document.querySelectorAll(".functionality-description");
let currentFunctionality = 0;

functionalitiesList.forEach((functionality) => {
    functionality.addEventListener("click", (event) => {
        // Usuń klasę active z wszystkich funkcjonalności
        functionalitiesList.forEach((item) => {
            if (item.classList.contains("active")) {
                item.classList.remove("active");
            }
        });
        // Dodaj klasę active do klikniętej funkcjonalności
        event.currentTarget.classList.add("active");
        // Pobierz id funkcjonalności
        currentFunctionality = event.currentTarget.dataset.functionality;

        // Schowaj opisy
        functionalityDescriptions.forEach((description) => {
            description.classList.add("d-none");
        });
        // Pokaż opis wybranej funkcjonalności
        functionalityDescriptions[currentFunctionality].classList.remove("d-none");
        functionalityDescriptions[currentFunctionality].classList.add("d-flex");
    });
});