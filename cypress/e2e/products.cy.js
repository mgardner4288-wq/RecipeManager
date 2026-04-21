describe("Edit Product Authorization", () => {
	it("does NOT show edit button when NOT logged in", () => {
		cy.visit("/");

		cy.contains("Edit").should("not.exist");
	});

	it("shows edit button when logged in", () => {
		cy.visit("/");

		cy.get("input#email").type("test@test.com");
		cy.get("input#password").type("1234");
		cy.contains("Log In").click();

		cy.contains("Edit").should("be.visible");
	});

	it("does NOT show delete button when NOT logged in", () => {
		cy.visit("/Recipes");

		cy.contains("Delete").should("not.exist");
	});

	it("shows delete button when logged in", () => {
		cy.visit("/");

		cy.get("input#email").type("test@test.com");
		cy.get("input#password").type("1234");
		cy.contains("Log In").click();

		cy.contains("Products");
		cy.contains("Delete").should("be.visible");
	});
});
