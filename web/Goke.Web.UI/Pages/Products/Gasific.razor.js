
export function plot4(element, data) {
    const plot = Plot.plot({
        //marginTop: 50,
        //marginRight: 50,
        //marginBottom: 50,
        //marginLeft: 100,
        marks: [
            Plot.barX(data, { x: "outlet", y: "formula", fill: "green" }),
        ]
    });
    const div = document.querySelector(element);
    div.append(plot);
    div.replaceChildren(plot);
}

export function alertUser() {
    alert('The button was selected!');
}

export function addHandlers() {
    const btn = document.getElementById("btn");
    btn.addEventListener("click", alertUser);
}