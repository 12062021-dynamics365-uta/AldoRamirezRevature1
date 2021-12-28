// Todo List

//create the input element
let inputElem = document.createElement('input');
//Add the element to the body
document.body.appendChild(inputElem);

//create the submit element
let submitTodo = document.createElement('button');
document.body.appendChild(submitTodo);
submitTodo.innerText = 'Submit A New Todo!';

//create the title element for the list
let title = document.createElement('h1');
document.body.appendChild(title);
title.innerText = 'Your Todos!';

let todoList = document.createElement('ul');
document.body.appendChild(todoList);
//todoList.innerHTML = `<li> This is the first list item</li>`;
//todoList.innerHTML += `<li> This is the second list item</li>`;

let myUl = document.querySelector('ul');
myUl.classList.add('ulClass');

submitTodo.addEventListener('click', (e) => {
    let text = inputElem.value;
    let myLi = document.createElement('li');
    myLi.innerText = `${text}`;
    myUl.appendChild(myLi);
    inputElem.value = '';
    inputElem.focus();
    //todoList.innerHTML += `<li>${text}</li>`;
});