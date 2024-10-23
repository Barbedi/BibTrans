const functionalitiesList = document.querySelectorAll(".functionalities-list li");
const functionalityDescriptions = document.querySelectorAll(".functionality-description");
let currentFunctionality = 0;
let activeFunctionality = document.querySelector(".functionalities-list li.active");

functionalitiesList.forEach((functionality) => {
    functionality.addEventListener("click", (event) => {
        // Usuń klasę active z poprzedniej funkcjonalności
        if (activeFunctionality) {
            activeFunctionality.classList.remove("active");
        }
        // Dodaj klasę active do klikniętej funkcjonalności
        event.currentTarget.classList.add("active");
        activeFunctionality = event.currentTarget;

        // Pobierz id funkcjonalności
        currentFunctionality = event.currentTarget.dataset.functionality;

        // Schowaj aktualnie widoczny opis
        const visibleDescription = document.querySelector(".functionality-description.d-flex");
        if (visibleDescription) {
            visibleDescription.classList.remove("d-flex");
            visibleDescription.classList.add("d-none");
        }

        // Pokaż opis wybranej funkcjonalności
        const selectedDescription = functionalityDescriptions[currentFunctionality];
        selectedDescription.classList.remove("d-none");
        selectedDescription.classList.add("d-flex");
    });
});
