describe('Login Page Tests', () => {
  it('should show login form when not logged in', () => {
    cy.visit('http://localhost:5043')

    cy.contains('Login').should('be.visible')
    cy.get('input#email').should('be.visible').and('have.attr', 'required')
    cy.get('input#password').should('be.visible').and('have.attr', 'required')
  })
})

it('should log in successfully', () => {
  cy.visit('http://localhost:5043')

  cy.get('input#email').type('test@test.com')
  cy.get('input#password').type('1234')
  cy.contains('Log In').click()

  cy.contains('Welcome, test@test.com!').should('be.visible')
})

it('should show logout button after login', () => {
  cy.visit('http://localhost:5043')

  cy.get('input#email').type('test@test.com')
  cy.get('input#password').type('1234')
  cy.contains('Log In').click()

  cy.contains('Log Out').should('be.visible')
})

it('should show create product only when logged in', () => {
  cy.visit('http://localhost:5043')

  // Not logged in → should NOT see it
  cy.contains('Create Product').should('not.exist')

  // Log in
  cy.get('input#email').type('test@test.com')
  cy.get('input#password').type('1234')
  cy.contains('Log In').click()

  // Now it SHOULD exist
  cy.contains('Create Product').should('be.visible')
})
