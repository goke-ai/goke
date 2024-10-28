

export function plot4(element, data) {

    console.log(globalThis);
    console.log(window);

    const plot = Plot.plot({
        //marginTop: 50,
        //marginRight: 50,
        marginBottom: 40,
        marginLeft: 60,
        //width: 640,
        //width: Math.max(innerWidth, 550),
        //height: 300,
        height: data.length * 40,
        responsive: true,
        //style: {
        //    fontSize: "16px", // Adjust the font size here
        //},
        marks: [
            Plot.barX(data, { x: "outlet", y: "formula", fill: "green" }),
        ]
    });
    const div = document.querySelector(element);
    div.append(plot);
    div.replaceChildren(plot);
}

export function plotly4(element, data2) {

    const y = data2.map((v, i) => v.formula)
    const x = data2.map((v, i) => v.outlet)

    var data = [{
        y: y, //['Marc', 'Henrietta', 'Jean', 'Claude', 'Jeffrey', 'Jonathan', 'Jennifer', 'Zacharias'],
        x: x, //[90, 40, 60, 80, 75, 92, 87, 73],
        type: 'bar',
        orientation: 'h'
    }]

    var layout = {
        //title: 'Always Display the Modebar',
        showlegend: false
    }

    var config = {
        responsive: true,
        //displayModeBar: true
    }

    const div = document.querySelector(element);

    Plotly.newPlot(div, data, layout, config)
}

export function alertUser() {
    alert('The button was selected!');
}

export function addHandlers() {
    //const btn = document.getElementById("btn");
    //btn.addEventListener("click", alertUser);
}