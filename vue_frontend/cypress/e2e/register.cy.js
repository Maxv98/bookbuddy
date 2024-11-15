describe('Test Bookbuddy registration', () => {
    it('registers a Bookbuddy', () => {
        cy.visit('http://localhost:5173')
        cy.url().should('include', '/bookbuddy/register')

        cy.get('#username').type('testuser')
        cy.get('#email').type('testuser@example.com')
        cy.get('#password').type('password123')

        cy.get('form').submit() 

        cy.get('.popup-overlay.popup-show').should('be.visible')
        cy.get('.popup-content .popup-text').should('contain', 'Account successfully registered!')

        cy.get('.popup-content button').click()
        cy.get('.popup').should('not.exist')

        cy.url().should('include', '/bookbuddy/')

        cy.get('#username').should('have.value', 'testuser')
        cy.get('#email').should('have.value', 'testuser@example.com')
        cy.get('#password').should('have.value', 'password123')
    })
})