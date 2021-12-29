let oneJokeButton = document.createElement('button');
document.body.appendChild(oneJokeButton);
oneJokeButton.innerText = 'Push For Joke';

let xhr = new XMLHttpRequest();
oneJokeButton.addEventListener('click', (event) => {
    xhr.onreadystatechange = () => {
        console.log(`The ready state is ${xhr.readyState}`);

        if(xhr.readyState == 4)
        {
            console.log(`The response is ${xhr.responseText}`);

            //Code to render the joke only to the browser.
            let myDiv = document.createElement('div');
            let myPar = document.createElement('p');
            myDiv.appendChild(myPar);
            document.body.appendChild(myDiv);
            let response = JSON.parse(xhr.responseText);
            myPar.innerText = response.value.joke;
        }
    };
    xhr.open('GET', 'http://api.icndb.com/jokes/random');
    xhr.send();
});

let fiveJokeButton = document.createElement('button');
document.body.appendChild(fiveJokeButton);
fiveJokeButton.innerText = 'Push For 5 Jokes';
let myDiv = document.createElement('div');

fiveJokeButton.onclick = () => {
    fetch('http://api.icndb.com/jokes/random/5')
    .then((res, err) => {
        if(err) {
            console.log(`There was an error in the request ${err}`);
        }
        else {
            document.body.appendChild(myDiv);
            //myPar.innerText = res.value.joke
            return res.json();
        }
    })
    .then(res => {
        for(let i = 0; i < res.value.length; i++) {
            let myPar = document.createElement('p');
            myDiv.appendChild(myPar);
            myPar.innerText = res.value[i].joke;
        }
    });
};