import { CommonFixture } from "./models/common-fixture";

export class SharedTest
{
  constructor(private commonFixturePath: string)
  {

  }

  visit(): Cypress.Chainable
  {
    return cy.fixture(this.commonFixturePath).then((v: CommonFixture) => {
        if(v.baseUrl === undefined)
          throw 'BaseUrl is undefined';

        cy.visit(v.baseUrl + '/')
      });
  }

  visitAndLogin(): Cypress.Chainable
  {
    return cy.fixture(this.commonFixturePath).then((v: CommonFixture) => {
      this.visit().then(() => {
        if(v.username === undefined || v.password === undefined)
          throw 'User or Password is undefined';

        cy.get('input[name="login"]').type(v.username);
        cy.get('input[name="password"]').type(v.password);
        cy.get('div.actions > button').click();
      })
    });
  }
}
