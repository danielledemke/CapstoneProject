{
    var myLineChart = new Chart(ctxL, {
        type: 'line',
        data: {
            labels: ["January", "February", "March", "April", "May", "June", "July"],
            datasets: [
                {
                    label: "Sales(in US dollars)",
                    data: [0, 45, 89, 148, 60, 0, 105],
                    backgroundColor: '#AD35BA',
                    borderColor: [
                        '#AD35BA',
                    ],
                    borderWidth: 2,
                    pointBorderColor: "#fff",
                    pointBackgroundColor: "rgba(173, 53, 186, 0.1)",
                }
            ]
        },
        options: {
            responsive: true
        }
    });
}

