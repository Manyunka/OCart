// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function chpok(id) {
    elem = document.getElementById(id); //находим блок div по его id, который передали в функцию

    if (elem.style.display != 'none') //смотрим, включен ли сейчас элемент
        elem.style.display = 'none'; //если включен, то выключаем
    else
        elem.style.display = 'block'; //иначе - включаем
}

var onclick = () => {
    document.getElementById('demo').innerHTML = Date.now().toLocaleString()
}