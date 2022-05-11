import { getToken } from "./authManager";
const _apiUrl = "/api/shoe";

export const getAllShoes = () => {
    return getToken().then((token) =>
      fetch(`${_apiUrl}`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }).then((res) => {
        if (res.ok) {
          return res.json();
        } else {
          throw new Error("Error");
        }
      })
    );
  };

  export const addShoe = (shoe) => {
    return getToken().then((token) =>
      fetch(_apiUrl, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(shoe),
      }).then((res) => {
        if (res.ok) {
          return res.json();
        } else {
          throw new Error("Error");
        }
      })
    );
  };

  export const getShoeById = (id) => {
    return getToken().then((token) => {
        return fetch(`${_apiUrl}/${id}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((res) => {
            if (res.ok) {
                return res.json();
            } else {
                throw new Error("Unknown error getting posts");
            }
        });
    });
};

export const getShoe = (id) => {
  return getToken().then((token) =>
    fetch(`${_apiUrl}/${id}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }).then((res) => {
      if (res.ok) {
        return res.json();
      } else {
        throw new Error("Error");
      }
    })
  );
};

export const editShoe = (shoe) => {
  return getToken().then((token) => {
    fetch(`${_apiUrl}/${shoe.id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(shoe),
    }).then((res) => {
      if (res.ok) {
        return res.status;
      } else {
        throw new Error("Error");
      }
    });
  });
}

export const getShoesByCollectionId = (id) => {
  return getToken().then((token) =>
    fetch(`${_apiUrl}/GetWithCollections/${id}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }).then((res) => {
      if (res.ok) {
        return res.json();
      } else {
        throw new Error("Error");
      }
    })
  );
};

export const getTagsByShoeId = (id) => {
  return getToken().then((token) =>
    fetch(`${_apiUrl}/GetWithTags/${id}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }).then((res) => {
      if (res.ok) {
        return res.json();
      } else {
        throw new Error("Error");
      }
    })
  );
};

export const deleteShoe = (id) => {
  return getToken().then((token) => {
    return fetch(`${_apiUrl}/${id}`, {
      method: "DELETE",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
  });
};

export const addShoeTags = (shoe) => {
  return getToken().then((token) => {
    return fetch(`${_apiUrl}/`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(shoe),
    }).then((res) => {
      if (res.ok) {
        return res.json();
      } else {
        throw new Error(
          "An unknown error occurred while trying to save new tags for the shoe."
        );
      }
    });
  });
};