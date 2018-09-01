const endpoint = window["endpoint"];
export class Constants {
  static apiContacts = endpoint + "/api/contacts";
  static apiTags = endpoint + "/api/tags";
  static hubTags = endpoint + "/hub/tags";

  static paginationOptions = {
    itemsPerPage: 15,
    pageSizeOptions: [15]
  };
}
