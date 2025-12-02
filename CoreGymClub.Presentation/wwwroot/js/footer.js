
//Weather code.
const apiKey = '4548d19a301df0eed7c73110e6d8f49e';
const city = 'Stockholm';

async function fetchWeather() {
    const url = `https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=${apiKey}&units=metric`;

    try {
        const response = await fetch(url);
        const data = await response.json();

        console.log('Weather data:', data);

        const temp = Math.round(data.main.temp);
        const iconCode = data.weather[0].icon;
        const iconUrl = `https://openweathermap.org/img/wn/${iconCode}@2x.png`;

        document.getElementById('temperature').textContent = `${temp}°C`;
        document.getElementById('weather-icon').innerHTML = `<img src="${iconUrl}" alt="Weather Icon" style="height: 40px;">`;



    } catch (error) {
        console.error('Weather fetch failed:', error);
        document.getElementById('temperature').textContent = 'N/A';
    }
}

fetchWeather();