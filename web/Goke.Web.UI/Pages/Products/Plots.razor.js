

export function plotBar(element) {
    const plot = Plot.rectY({ length: 10000 },
                             Plot.binX({ y: "count" }, { x: Math.random })
                            ).plot();

    const div = document.querySelector(element);
    div.append(plot);
}

export function plot1(element) {
    const plot = Plot.plot({
        title: "For charts, an informative title",
        subtitle: "Subtitle to follow with additional context",
        caption: "Figure 1. A chart with a title, subtitle, and caption.",
        marks: [
            Plot.frame(),
            Plot.text(["Titles, subtitles, captions, and annotations assist inter­pretation by telling the reader what’s interesting. Don’t make the reader work to find what you already know."], { lineWidth: 30, frameAnchor: "middle" })
        ]
    })
    const div = document.querySelector(element);
    div.append(plot);
}

export function plot2(element, data) {
    let aapl = [
        { Date: new Date("2013-05-13"), Open: 64.501427, High: 65.414284, Low: 64.500000, Close: 64.962860, Volume: 79237200 },
        { Date: new Date("2013-05-14"), Open: 64.835716, High: 65.028572, Low: 63.164288, Close: 63.408573, Volume: 111779500 },
        { Date: new Date("2013-05-15"), Open: 62.737144, High: 63.000000, Low: 60.337143, Close: 61.264286, Volume: 185403400 },
        { Date: new Date("2013-05-16"), Open: 60.462856, High: 62.549999, Low: 59.842857, Close: 62.082859, Volume: 150801000 },
        { Date: new Date("2013-05-17"), Open: 62.721428, High: 62.869999, Low: 61.572857, Close: 61.894287, Volume: 106976100 }
    ];

    const data0 = data.map((v, i) => {
        return { Symbol: v.symbol, Date: new Date(v.date), Open: v.open, High: v.high, Low: v.low, Close: v.close, Volume: v.volume }
    });

    const plot = Plot.plot({
        marks: [
            Plot.lineY(data0, { x: "Date", y: "Close" })
        ]
    });
    const div = document.querySelector(element);
    div.append(plot);
}

export function plot3(element, data) {
    let aapl = [
        { Date: new Date("2013-05-13"), Open: 64.501427, High: 65.414284, Low: 64.500000, Close: 64.962860, Volume: 79237200 },
        { Date: new Date("2013-05-14"), Open: 64.835716, High: 65.028572, Low: 63.164288, Close: 63.408573, Volume: 111779500 },
        { Date: new Date("2013-05-15"), Open: 62.737144, High: 63.000000, Low: 60.337143, Close: 61.264286, Volume: 185403400 },
        { Date: new Date("2013-05-16"), Open: 60.462856, High: 62.549999, Low: 59.842857, Close: 62.082859, Volume: 150801000 },
        { Date: new Date("2013-05-17"), Open: 62.721428, High: 62.869999, Low: 61.572857, Close: 61.894287, Volume: 106976100 }
    ];

    const data0 = data[0].map((v, i) => {
        return { Symbol: v.symbol, Date: new Date(v.date), Open: v.open, High: v.high, Low: v.low, Close: v.close, Volume: v.volume }
    });
    const data1 = data[1].map((v, i) => {
        return { Symbol: v.symbol, Date: new Date(v.date), Open: v.open, High: v.high, Low: v.low, Close: v.close, Volume: v.volume }
    });

    const plot = Plot.plot({
        marks: [
            Plot.ruleY([0]),
            Plot.lineY(data0, { x: "Date", y: "Close", stroke: "red" }),
            Plot.lineY(data1, { x: "Date", y: "Close", stroke: "blue" })
        ]
    });
    const div = document.querySelector(element);
    div.append(plot);
}

export function plot4(element, data) {
    let alphabet = [
        { letter: "A", frequency: 3 },
        { letter: "B", frequency: 4 },
        { letter: "C", frequency: 1 },
    ];

    const plot = Plot.plot({
        x: { padding: 0.4 },
        marks: [
            Plot.barY(data, { x: "letter", y: "frequency", dx: 2, dy: 2 }),
            Plot.barY(data, { x: "letter", y: "frequency", fill: "green", dx: -2, dy: -2 })
        ]
    });
    const div = document.querySelector(element);
    div.append(plot);
}

export function plot5(element, data) {
    
    const plot = Plot.plot({
        marginTop: 16,
        marginRight: 20,
        marginBottom: 30,
        marginLeft: 40,
        grid: true,
        marks: [
            Plot.barY(data, { x: "letter", y: "frequency", fill: "green" }),
            Plot.frame()
        ]
    });
    const div = document.querySelector(element);
    div.append(plot);
}
export function plot6(element, data) {
    const aapl = [
        { Symbol: "AAPL", Date: new Date("2013-05-13"), Open: 64.501427, High: 65.414284, Low: 64.500000, Close: 64.962860, Volume: 79237200 },
        { Symbol: "AAPL", Date: new Date("2013-05-14"), Open: 64.835716, High: 65.028572, Low: 63.164288, Close: 63.408573, Volume: 111779500 },
        { Symbol: "AAPL", Date: new Date("2013-05-15"), Open: 62.737144, High: 63.000000, Low: 60.337143, Close: 61.264286, Volume: 185403400 },
        { Symbol: "AAPL", Date: new Date("2013-05-16"), Open: 60.462856, High: 62.549999, Low: 59.842857, Close: 62.082859, Volume: 150801000 },
        { Symbol: "AAPL", Date: new Date("2013-05-17"), Open: 62.721428, High: 62.869999, Low: 61.572857, Close: 61.894287, Volume: 106976100 },
        { Symbol: "GOOG", Date: new Date("2013-05-13"), Open: 64.501427, High: 65.414284, Low: 64.500000, Close: 24.962860, Volume: 79237200 },
        { Symbol: "GOOG", Date: new Date("2013-05-14"), Open: 64.835716, High: 65.028572, Low: 63.164288, Close: 23.408573, Volume: 111779500 },
        { Symbol: "GOOG", Date: new Date("2013-05-15"), Open: 62.737144, High: 63.000000, Low: 60.337143, Close: 21.264286, Volume: 185403400 },
        { Symbol: "GOOG", Date: new Date("2013-05-16"), Open: 60.462856, High: 62.549999, Low: 59.842857, Close: 22.082859, Volume: 150801000 },
        { Symbol: "GOOG", Date: new Date("2013-05-17"), Open: 62.721428, High: 62.869999, Low: 61.572857, Close: 21.894287, Volume: 106976100 }
    ];

    const data6 = data.map((v, i) => {
        return { Symbol: v.symbol, Date: new Date(v.date), Open: v.open, High: v.high, Low: v.low, Close: v.close, Volume: v.volume }
    });

    const plot = Plot.plot({
        color: { legend: true },
        marks: [
            Plot.ruleY([0]),
            Plot.lineY(data6, { x: "Date", y: "Close", stroke: "Symbol" })
        ]
    });
    const div = document.querySelector(element);
    div.append(plot);
}

export function plot7(element, penguins) {
    console.log(penguins);

    const plot = Plot.plot({
        grid: true,
        inset: 10,
        //aspectRatio: fixed ? 1 : undefined,
        color: { legend: true },
        marks: [
            Plot.frame(),
            Plot.dot(penguins, { x: "culmen_length_mm", y: "culmen_depth_mm", stroke: "species" })
        ]
    });

    const div = document.querySelector(element);
    div.append(plot);
}

export function alertUser() {
    alert('The button was selected!');
}

export function addHandlers() {
    const btn = document.getElementById("btn");
    btn.addEventListener("click", alertUser);
}