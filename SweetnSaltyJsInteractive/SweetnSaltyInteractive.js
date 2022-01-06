//title setup
let title = document.createElement('h1');
document.body.appendChild(title);
title.innerText = "Welcome to Sweet'nSalty!";

//instructions setup


//start button setup
let startBtn = document.createElement('button');
document.body.appendChild(startBtn);
startBtn.innerText = 'Start Sweet\'nSalty';


let startText = document.createElement('h3');
startText.innerText = "Enter a number to start with:";

let startElem = document.createElement('input');
let submitStart = document.createElement('button');

startBtn.addEventListener('click', (event) => {
    document.body.innerHTML = '';
    document.body.appendChild(startText);
    document.body.appendChild(startElem);
    document.body.appendChild(submitStart);
    submitStart.innerText = 'Enter';
});

let endText = document.createElement('h3');
endText.innerText = "Enter a number to end with:";

let endElem = document.createElement('input');
let submitEnd = document.createElement('button');

let startNum = 0;

submitStart.addEventListener('click', (event) => {
    document.body.innerHTML = '';
    let text = startElem.value;
    
    if(validateInput(text))
    {
        startNum = parseInt(text);
    }
    
    document.body.appendChild(endText);
    document.body.appendChild(endElem);
    document.body.appendChild(submitEnd);
    submitEnd.innerText = 'Enter';
});

let endNum = 0;

submitEnd.addEventListener('click', (event) => {
    document.body.innerHTML = '';
    let text = endElem.value;
    
    if(validateInput(text))
        endNum = parseInt(text);
    
    printSweetNSalty(startNum, endNum);
});

function validateInput(text) {
    if(text.trim().length != 0)
    {
        if(parseInt(text) < 0)
            return false;
        else
            return true;
    }
    return false;
}

function printSweetNSalty(startNum, endNum)
{
    let sweetCnt = 0; //sweet count
    let saltyCnt = 0; //salty count
    let sweetNSalty = 0; //sweet'nSalty count
    let groupCount = 0; //Group count
    let line = '';
    let para = document.createElement('p');
    document.body.appendChild(para);
    
    for(let i = startNum; i <= endNum; i++) //Loop from 1 to 1000
    {
        groupCount++; //Increment group count
        if (i % 3 == 0 && i % 5 == 0) //Multiples of both 3 and 5
        { 
            let span = document.createElement('span');
            span.classList.add("sweetNSaltyClass")
            span.textContent = "sweet'nSalty ";
            para.append(span); //Concat sweet'nSalty
            sweetNSalty++; //Increment sweet'nSalty count
        }
        else if (i % 3 == 0) //Multiples of only 3
        {
            let span = document.createElement('span');
            span.classList.add("sweetNSaltyClass")
            span.textContent = "sweet ";
            para.append(span); //Concat sweet
            sweetCnt++; //Increment sweet count
        }
        else if (i % 5 == 0) //Multiples of only 5
        {
            let span = document.createElement('span');
            span.classList.add("sweetNSaltyClass")
            span.textContent = "salty ";
            para.append(span); //Concat salty
            saltyCnt++; //Increment salty count
        }
        else //Not multiple of either 3 or 5
        {
            if (i > 999) //If greater than 999 add coma
                para.append(`${i.toLocaleString()} `); //Concat number 
            else
                para.append(`${i} `); //Concat number
        }
        
        if (groupCount == 40 || i == endNum) //Number of concats reaches 40
        {
            para.append("\n");
            groupCount = 0; //Reset group count
            para = document.createElement('p');
            document.body.appendChild(para);
        }
    }
    para = document.createElement('p');
    document.body.appendChild(para);
    para.append(`Total sweet: ${sweetCnt}\n`);

    para = document.createElement('p');
    document.body.appendChild(para);
    para.append(`Total salty: ${saltyCnt}\n`);

    para = document.createElement('p');
    document.body.appendChild(para);
    para.append(`Total sweet'nSalty: ${sweetNSalty}\n`);
}
