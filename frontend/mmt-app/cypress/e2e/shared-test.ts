export class SharedTest
{
  private baseUrl: string | undefined;
  private user: string | undefined;
  private password: string | undefined;

  constructor(commonFixturePath: string)
  {
    cy.fixture(commonFixturePath).then(data => {
      this.baseUrl = data.baseUrl;
      this.user = data.username;
      this.password = data.password;
    });
  }

  visit()
  {
    if(this.baseUrl === undefined)
      throw 'BaseUrl is undefined';

    cy.visit(this.baseUrl + '/');
  }

  visitAndLogin()
  {
    if(this.user === undefined || this.password === undefined)
      throw 'User or Password is undefined';

    this.visit();

    cy.get('input[name="login"]').type(this.user);
    cy.get('input[name="password"]').type(this.password);
    cy.get('div.actions > button').click();
  }
}
