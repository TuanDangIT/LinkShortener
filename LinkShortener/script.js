import axios from "./node_modules/axios/dist/esm/axios.js";
const form = document.querySelector("form");
const headerText = document.querySelector(".header-text");
const BASE_URL = "http://localhost:8080";
form.addEventListener("submit", async e => {
	e.preventDefault();
	const formInput = form.querySelector(".form-input");
	const url = formInput.value;
	console.log(url);
	try {
		const response = await axios.post(`${BASE_URL}/shorten?url=${url}`);
		formInput.value = "";
		headerText.textContent = `Your shortened URL: ${response.data}`;
	} catch (error) {
		formInput.value = "";
		console.log(error);
	}
});
