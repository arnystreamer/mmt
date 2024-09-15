import { SharedTest } from "./shared-test";

describe('Visit and Login Test', () => {

  let sharedTest: SharedTest;

  before(() =>
  {
    sharedTest = new SharedTest('common');
  })

  it('Visits the initial project page', () => {
    sharedTest.visit();
    cy.contains('Login');
  })

  it('Should login', () => {
    sharedTest.visitAndLogin();
    cy.contains('Start page');
  })
})
