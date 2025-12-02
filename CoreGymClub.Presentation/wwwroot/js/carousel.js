
const carouselInner = document.querySelector('.carousel-inner-container');
carouselInner.addEventListener('animationend', () => {
    moveStepLeft();
});
carouselInner.addEventListener('mouseenter', () => {
    carouselInner.style.animationPlayState = 'paused';
});

carouselInner.addEventListener('mouseleave', () => {
    carouselInner.style.animationPlayState = 'running';
});


//let imgbox1 = document.querySelector('.carousel-imgbox-1');
//let imgbox2 = document.querySelector('.carousel-imgbox-2');
//let imgbox3 = document.querySelector('.carousel-imgbox-3');
//let imgbox4 = document.querySelector('.carousel-imgbox-4');
//let imgbox5 = document.querySelector('.carousel-imgbox-5');

let imgbox1 = document.getElementById('carousel-imgbox-1-id');
let imgbox2 = document.getElementById('carousel-imgbox-2-id');
let imgbox3 = document.getElementById('carousel-imgbox-3-id');
let imgbox4 = document.getElementById('carousel-imgbox-4-id');
let imgbox5 = document.getElementById('carousel-imgbox-5-id');

let icon1 = imgbox1.querySelector('#carousel-icon-id-1');
let icon2 = imgbox2.querySelector('#carousel-icon-id-2');
let icon3 = imgbox3.querySelector('#carousel-icon-id-3');
let icon4 = imgbox4.querySelector('#carousel-icon-id-4');
let icon5 = imgbox5.querySelector('#carousel-icon-id-5');

let text1 = imgbox1.querySelector('#carousel-text-id-1');
let text2 = imgbox2.querySelector('#carousel-text-id-2');
let text3 = imgbox3.querySelector('#carousel-text-id-3');
let text4 = imgbox4.querySelector('#carousel-text-id-4');
let text5 = imgbox5.querySelector('#carousel-text-id-5');





let imageboxes = [imgbox1, imgbox2, imgbox3, imgbox4, imgbox5];

let imgboxClasses = ["carousel-imgbox-1", "carousel-imgbox-2", "carousel-imgbox-3", "carousel-imgbox-4"];

let iconClasses = ["carousel-icon-1", "carousel-icon-2", "carousel-icon-3", "carousel-icon-4"];

let textClasses = ["carousel-text-1", "carousel-text-2", "carousel-text-3", "carousel-text-4"];

let carouselTexts = [
    "Se vårt senaste\ntillskott!",                // for carousel-imgbox-1
    "Upptäck Yoga\nmed vår nya tränare!",                // for carousel-imgbox-2
    "Glöm inte\nvattenflaskan!", // for carousel-imgbox-3
    "Samla morgonpass med\nEarly Bird!"                 // for carousel-imgbox-4
];

let classIndex = 0;

//setTimeout(() => {
    //moveStepLeft();
//}, 8000);

let boxIndexes = [0, 1, 2, 3, 0]; // 5 boxes, first and last are the same

function moveStepLeft() {

    restartAnimation(carouselInner, 'moveLeft');

    

    // Remove current classes from background images
    imgbox1.classList.remove(imgboxClasses[boxIndexes[0]]);
    imgbox2.classList.remove(imgboxClasses[boxIndexes[1]]);
    imgbox3.classList.remove(imgboxClasses[boxIndexes[2]]);
    imgbox4.classList.remove(imgboxClasses[boxIndexes[3]]);
    imgbox5.classList.remove(imgboxClasses[boxIndexes[4]]);

    // Remove current classes from icons
    icon1.classList.remove(iconClasses[boxIndexes[0]]);
    icon2.classList.remove(iconClasses[boxIndexes[1]]);
    icon3.classList.remove(iconClasses[boxIndexes[2]]);
    icon4.classList.remove(iconClasses[boxIndexes[3]]);
    icon5.classList.remove(iconClasses[boxIndexes[4]]);

    // Remove current classes from texts
    text1.classList.remove(textClasses[boxIndexes[0]]);
    text2.classList.remove(textClasses[boxIndexes[1]]);
    text3.classList.remove(textClasses[boxIndexes[2]]);
    text4.classList.remove(textClasses[boxIndexes[3]]);
    text5.classList.remove(textClasses[boxIndexes[4]]);

    // Shift indexes left and wrap correctly
    for (let i = 0; i < boxIndexes.length; i++) {
        boxIndexes[i] = (boxIndexes[i] + 1) % imgboxClasses.length; // 4 images
    }

    console.log(boxIndexes);

    // Add new classes
    imgbox1.classList.add(imgboxClasses[boxIndexes[0]]);
    imgbox2.classList.add(imgboxClasses[boxIndexes[1]]);
    imgbox3.classList.add(imgboxClasses[boxIndexes[2]]);
    imgbox4.classList.add(imgboxClasses[boxIndexes[3]]);
    imgbox5.classList.add(imgboxClasses[boxIndexes[4]]);

    // Add current classes from icons
    icon1.classList.add(iconClasses[boxIndexes[0]]);
    icon2.classList.add(iconClasses[boxIndexes[1]]);
    icon3.classList.add(iconClasses[boxIndexes[2]]);
    icon4.classList.add(iconClasses[boxIndexes[3]]);
    icon5.classList.add(iconClasses[boxIndexes[4]]);

    // add current classes to texts
    text1.classList.add(textClasses[boxIndexes[0]]);
    text2.classList.add(textClasses[boxIndexes[1]]);
    text3.classList.add(textClasses[boxIndexes[2]]);
    text4.classList.add(textClasses[boxIndexes[3]]);
    text5.classList.add(textClasses[boxIndexes[4]]);

    // Set text content dynamically
    text1.textContent = carouselTexts[boxIndexes[0]];
    text2.textContent = carouselTexts[boxIndexes[1]];
    text3.textContent = carouselTexts[boxIndexes[2]];
    text4.textContent = carouselTexts[boxIndexes[3]];
    text5.textContent = carouselTexts[boxIndexes[4]];



}
function restartAnimation(element, animationName, duration = '6s', timing = 'linear') {
    element.style.animation = 'none';
    void element.offsetWidth; // force reflow
    element.style.animation = `${animationName} ${duration} ${timing}`;
}


