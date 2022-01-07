runSweetNSalty();

function runSweetNSalty()
{
    let br = document.createElement('br');

    //title setup
    let title = document.createElement('h1');
    document.body.appendChild(title);
    title.innerText = "Welcome to Sweet'nSalty!";

    //instructions setup
    let instHead = document.createElement('h2');
    document.body.appendChild(instHead);
    instHead.innerText += 'Instructions';

    let instPara = document.createElement('p');
    document.body.appendChild(instPara);
    instPara.append('Enter a number to start with: Must be greater than 0')
    
    instPara = document.createElement('p');
    document.body.appendChild(instPara);
    instPara.append('Enter a number to end with: Must be at least 200 apart from starting number')

    instPara = document.createElement('p');
    document.body.appendChild(instPara);
    instPara.append('Ending number also has to be no more than 10,000 apart from starting number')

    instPara = document.createElement('p');
    document.body.appendChild(instPara);
    instPara.append('Sweet\'nSalty will print every number inbetween the two numbers you chose')

    instPara = document.createElement('p');
    document.body.appendChild(instPara);
    instPara.append('For every multiple of 3 Sweet will be printed')

    instPara = document.createElement('p');
    document.body.appendChild(instPara);
    instPara.append('For every multiple of 5 Salty will be printed')

    instPara = document.createElement('p');
    document.body.appendChild(instPara);
    instPara.append('For every multiple of 3 and 5 Sweet\'nSalty will be printed')

    //start button setup
    let startBtn = document.createElement('button');
    document.body.appendChild(startBtn);
    startBtn.innerText = 'Start Sweet\'nSalty';

    //second view setup
    let startText = document.createElement('h3');
    startText.innerText = "Please enter a starting number";

    let startElem = document.createElement('input');
    let submitStart = document.createElement('button');


    //EventHandler to start Sweet'nSalty
    startBtn.addEventListener('click', (event) => {
        document.body.innerHTML = '';
        document.body.appendChild(startText);
        document.body.appendChild(startElem);
        document.body.appendChild(br);
        document.body.appendChild(submitStart);
        submitStart.innerText = 'Enter';
        startElem.focus();
    });

    //third view setup
    let endText = document.createElement('h3');
    endText.innerText = "Please enter an ending number";

    let endElem = document.createElement('input');
    let submitEnd = document.createElement('button');

    let error = document.createElement('p');
    error.classList.add("errorClass");

    let startNum = 0;

    //Event handler for starting number button
    submitStart.addEventListener('click', (event) => {
        let text = startElem.value;
        
        if(validateInput(text))
        {
            document.body.innerHTML = '';
            startNum = parseInt(text);
            document.body.appendChild(endText);
            document.body.appendChild(endElem);
            document.body.appendChild(br);
            document.body.appendChild(submitEnd);
            submitEnd.innerText = 'Enter';
            endElem.focus();
        }
        else
        {
            startElem.value = '';
            document.body.appendChild(error);
            error.innerText = "Invalid Input: Please try again!";
        }
        
    });

    //Event handler for starting number on enter
    startElem.addEventListener('keypress', (event) => {
        if(event.key === 'Enter')
        {
            let text = startElem.value;
        
            if(validateInput(text))
            {
                document.body.innerHTML = '';
                startNum = parseInt(text);
                document.body.appendChild(endText);
                document.body.appendChild(endElem);
                document.body.appendChild(br);
                document.body.appendChild(submitEnd);
                submitEnd.innerText = 'Enter';
                endElem.focus();
            }
            else
            {
                startElem.value = '';
                document.body.appendChild(error);
                error.innerText = "Invalid Input: Please try again!";
            }
        }
    });

    let endNum = 0;

    //Event handler for ending number button
    submitEnd.addEventListener('click', (event) => {
        let text = endElem.value;

        if(validateInput(text) && parseInt(text) - startNum >= 200
            && parseInt(text) - startNum < 10000)
        {
            document.body.innerHTML = '';
            endNum = parseInt(text);
            printSweetNSalty(startNum, endNum);
        }
        else
        {
            endElem.value = '';
            document.body.appendChild(error);
            error.innerText = "Invalid Input: Please try again!";
        }
    });

    //Event handler for ending number on enter
    endElem.addEventListener('keypress', (event) => {
        if(event.key === 'Enter')
        {
            let text = endElem.value;

            if(validateInput(text) && parseInt(text) - startNum >= 199
                && parseInt(text) - startNum < 10000)
            {
                document.body.innerHTML = '';
                endNum = parseInt(text);
                printSweetNSalty(startNum, endNum);
            }
            else
            {
                endElem.value = '';
                document.body.appendChild(error);
                error.innerText = "Invalid Input: Please try again!";
            }
        }
    });

    //Validate user input
    function validateInput(text) {
        if(text.trim().length != 0 && !isNaN(text))
        {
            if(parseInt(text) <= 0)
                return false;
            else
                return true;
        }
        return false;
    }

    let restartBtn = document.createElement('button');

    //Print sweet'nSalty
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
                span.textContent = "sweet'nSalty";
                para.append(span); //Concat sweet'nSalty
                sweetNSalty++; //Increment sweet'nSalty count
            }
            else if (i % 3 == 0) //Multiples of only 3
            {
                let span = document.createElement('span');
                span.classList.add("sweetNSaltyClass")
                span.textContent = "sweet";
                para.append(span); //Concat sweet
                sweetCnt++; //Increment sweet count
            }
            else if (i % 5 == 0) //Multiples of only 5
            {
                let span = document.createElement('span');
                span.classList.add("sweetNSaltyClass")
                span.textContent = "salty";
                para.append(span); //Concat salty
                saltyCnt++; //Increment salty count
            }
            else //Not multiple of either 3 or 5
            {
                if (i > 999) //If greater than 999 add coma
                para.append(`${i.toLocaleString()}`); //Concat number 
                else
                para.append(`${i}`); //Concat number
            }
            para.append(' ');
            if (groupCount == 40 || i == endNum) //Number of concats reaches 40
            {
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
        
        document.body.appendChild(restartBtn);
        restartBtn.innerText = "Restart";
    }

    //Event listener to restart
    restartBtn.addEventListener('click', (event) => {
        document.body.innerHTML = '';
        runSweetNSalty();
    });
}