import React, { useState} from "react";
import { useHistory } from "react-router-dom";
import { addCollection } from "../../modules/collectionManager";
import { Form, FormGroup, Label, Input, Button } from "reactstrap";

const CollectionForm = () => {
    const history = useHistory();
    const emptyCollection = {
        name: "",
    };

    const [collection, setCollection] = useState(emptyCollection);
    const handleInputChange = (evt) => {
        const value = evt.target.value;
        const key = evt.target.id;

        const collectionCopy = {...collection};

        collectionCopy[key] = value;
        setCollection(collectionCopy);
    };
    const handleSave = (evt) => {
        evt.preventDefault();
        addCollection(collection).then((c) => {
            history.push("/mycollections");
        });
    };

    return (
        <Form>
            <FormGroup>
                <Label for="name">Name</Label>
                <Input
                type="text"
                name="name"
                id="name"
                placeholder="Collection Name"
                value={collection.name}
                onChange={handleInputChange}
                />
            </FormGroup>
            <Button className="btn btn-primary" onClick={handleSave}>
        Submit
      </Button>
        </Form>
    );

};
export default CollectionForm;