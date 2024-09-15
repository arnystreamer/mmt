import { SharedTest } from "./shared-test";

describe('Visit and Login Test', () => {

  const sharedTest: SharedTest = new SharedTest('common');

  it('Visits the initial project page', () => {
    sharedTest.visit().then(() =>
      cy.contains('Login')
    );
  });

  it('Should login', () => {
    sharedTest.visitAndLogin().then(() =>
      cy.contains('Start page')
    );
  });
})
