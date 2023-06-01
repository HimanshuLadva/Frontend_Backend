// this js file is not included in any html file


// first try
const ul = document.getElementById('nv');
const listItems = ul.getElementsByTagName('li');
console.log(listItems.length);
for (let i = 0; i <= listItems.length - 1; i += 1) {

    console.log(listItems[i].innerText);
}
// Loop through the NodeList object.


function jsUpdateSize() {
    // Get the dimensions of the viewport
    var width = window.innerWidth ||
        document.documentElement.clientWidth ||
        document.body.clientWidth;

    
    const ul2 = document.getElementById('nv2');
    if (width <= 955) {
        for (let i = 0; i <= listItems.length - 1; i += 1) {
            // ul.removeChild(listItems[i]);
            listItems[i].classList.add('nav__item');
            listItems[i].classList.add('nav__link');
            ul2.appendChild(listItems[i]);
        }
       
    }
    
    // else if (width == 956 || width >= 956) { 
    //     const ule = document.getElementById('nv2');
    //     const listUp = ule.getElementsByTagName('li');

    //     console.log(listItems.length);
    //     for (let i = listUp.length - 1; i > listItems.length; i--) {
    //         listItems[i].classList.remove('nav__item');
    //         listItems[i].classList.remove('nav__link');
    //         ul.appendChild(listItems[i]);
    //         ule.removeChild(listItems[i]);   
    //     }
    // }

};


window.onload = jsUpdateSize;       // When the page first loads
window.onresize = jsUpdateSize;

// 2 try
const up_nav = document.getElementById('nv');
const lo_nav = document.getElementById('nv2');
const up_list = up_nav.getElementsByTagName('li');
const up_link = up_nav.getElementsByTagName('a');
// console.log("hello ="+up_list.length);  
// console.log("hello ="+up_list[0].innerHTML);  


function jsUpdateSize() {
    // Get the dimensions of the viewport
    var width = window.innerWidth ||
        document.documentElement.clientWidth ||
        document.body.clientWidth;
  
    console.log(up_list);
    if (width <= 955) {
        console.log(up_list);
        for (let i = 0; i <up_list.length; i++) {
            // console.log(up_list[i]);
            // ul.removeChild(listItems[i]);
            const nnode = up_link[i].cloneNode(true)
            nnode.classList.add('nav__link');
            nnode.classList.add('nav__item');
            // up_list[i].cloneNode
            console.log(lo_nav);
            lo_nav.appendChild(nnode);
            // up_nav.removeChild(up_list[i]);
        }
        up_nav.innerHTML=""
    }
    // console.log("hello "+up_nav.children.length);
    // if(width > 955 && lo_nav.children.length===7){
      
    //    for(let i=1;i<4;i++){
    //         console.log('hello',lo_nav.children.length-i,lo_nav.children)
    //         up_nav.appendChild(lo_nav.children[lo_nav.children.length-i]);
    //         lo_nav.children[lo_nav.children.length-i].classList.add('nav__link');
    //         lo_nav.children[lo_nav.children.length-i].classList.add('nav__item');
    //         lo_nav.remove(lo_nav.children.length-i);
    //     }
    // }
};


window.onload = jsUpdateSize;       // When the page first loads
window.onresize = jsUpdateSize;



// slide js 
let slideIndex = 1;
showSlides(slideIndex);

function plusSlides(n) {
    showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    let i;
    let slides = document.getElementsByClassName("mySlides");
    let dots = document.getElementsByClassName("dot");
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
    setTimeout(showSlides, 2000);
}



// slider 
var counter = 1;
setInterval(function () {
    document.getElementById('radio' + counter).checked = true;
    counter++;
    if (counter > 4) {
        counter = 1;
    }
}, 1000);