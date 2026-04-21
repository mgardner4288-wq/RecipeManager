const { defineConfig } = require("cypress");

module.exports = defineConfig({
	e2e: {
		baseUrl: "http://localhost:5043", // ← your port here
	},
});
