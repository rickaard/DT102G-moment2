"use strict";

const openBtn = document.querySelector('.menuBtn');
const closeBtn = document.querySelector('.closeBtn');

openBtn.addEventListener('click', () => {
    document.querySelector('.nav-items').style.height = '250px';
    document.querySelector('body').style.overflow = 'hidden';
})

closeBtn.addEventListener('click', () => {
    document.querySelector('.nav-items').style.height = '0px';
    document.querySelector('body').style.overflow = 'auto';
})



