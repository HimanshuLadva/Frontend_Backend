const dlt1 = document.getElementById("dlt1");
const dlt2 = document.getElementById("dlt");
const action1  = document.getElementById("action1");
const action2  = document.getElementById("action");

window.addEventListener("resize", function(e) {
    e.preventDefault();
    changeInClass();
});

let classCount = 0;
function changeInClass() {
    if(window.innerWidth <= 779) {
        dlt1.classList.remove("toggle");
        dlt2.classList.add("toggle");
        action1.classList.remove("toggle");
        action2.classList.add("toggle");
        dlt1.style.display = "none";
        action1.style.display = "none";

    } else if(window.innerWidth > 779) {
        dlt1.classList.add("toggle");
        dlt2.classList.remove("toggle");
        action1.classList.add("toggle");
        action2.classList.remove("toggle");
        dlt1.style.display = "";
        action1.style.display = "";
    }
}
changeInClass();

function myClose(data) {
    data.classList.add("hidden1");
}