
const carouselInner = document.querySelector('.carousel-inner-container');
carouselInner.addEventListener('animationend', () => {
    moveStepLeft();
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

let imageboxes = [imgbox1, imgbox2, imgbox3, imgbox4, imgbox5];

let imgboxClasses = ["carousel-imgbox-1", "carousel-imgbox-2", "carousel-imgbox-3", "carousel-imgbox-4"];

let classIndex = 0;

//setTimeout(() => {
    //moveStepLeft();
//}, 8000);

let boxIndexes = [0, 1, 2, 3, 0]; // 5 boxes, first and last are the same

function moveStepLeft() {

    restartAnimation(carouselInner, 'moveLeft');

    // Remove current classes
    imgbox1.classList.remove(imgboxClasses[boxIndexes[0]]);
    imgbox2.classList.remove(imgboxClasses[boxIndexes[1]]);
    imgbox3.classList.remove(imgboxClasses[boxIndexes[2]]);
    imgbox4.classList.remove(imgboxClasses[boxIndexes[3]]);
    imgbox5.classList.remove(imgboxClasses[boxIndexes[4]]);

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


}
function restartAnimation(element, animationName, duration = '3s', timing = 'linear') {
    element.style.animation = 'none';
    void element.offsetWidth; // force reflow
    element.style.animation = `${animationName} ${duration} ${timing}`;
}