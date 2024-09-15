import { StaticDataFixture } from "./models/static-data-fixture";
import { SharedTest } from "./shared-test";

describe('Visit and Login Test', () => {

  const sharedTest: SharedTest = new SharedTest('common');

  before(() =>
  {

  });

  beforeEach(() =>
  {
    sharedTest.visitAndLogin().then(() => {
      cy.get('mat-card[data-cy="to-static-data"]').click();
      //intercept and wait here
    });
  })

  it('Visit static data', () => {
    cy.contains('Global sections');
  })

  it('Create, get details and remove static data', () => {

    cy.fixture('static-data').then((v: StaticDataFixture) => {

      const sections = v.sections;

      if (!sections.length)
        throw 'Sections are empty';

      for(let i = 0; i < sections.length; i++)
      {
        const sectionName = sections[i].name;
        const sectionDescription = sections[i].description;

        cy.get('div.actions-panel > button').click();

        cy.get('input[name="name"]').type(sectionName);
        cy.get('input[name="description"]').type(sectionDescription);
        cy.get('div.actions > button[color="primary"]').click();
        //intercept and wait here

        cy.contains('div.list-item', sectionName)
          .as("lastListItem")
          .within($el => {
            cy.wrap($el).should('contain.text', sectionName).and('contain.text', sectionDescription);
            cy.get('div.actions > div.action-item[data-cy="to-info"]').click();
            //intercept and wait here
            cy.contains('Global section details');
          });



        cy.get('div.actions-panel > button[data-cy="action-back"]').click();
        //intercept and wait here
        cy.contains('Global sections');

        cy.get('@lastListItem')
          .within(el => {
            cy.get('div.actions > div.action-item[data-cy="action-delete"]')
              .click() //intercept and wait here
              .then(() => cy.get('@lastListItem').should("not.exist"))
          });
      }
    });
  });

  it("Ok to pass empty decription", () =>
  {

    cy.fixture('static-data').then((v: StaticDataFixture) => {

      const sections = v.sections;

      if (!sections.length)
          throw 'Sections are empty';

      const sectionName = sections[0].name;

      cy.get('div.actions-panel > button').click();

      cy.get('input[name="name"]').type(sectionName);
      cy.get('div.actions > button[color="primary"]').click();
      cy.contains('div.list-item', sectionName)
        .as("lastListItem")
        .within(el => {
          cy.get('div.actions > div.action-item[data-cy="action-delete"]')
            .click()
            .then(() => cy.get('@lastListItem').should("not.exist"))
        });
    });
  });
})
