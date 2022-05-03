import React, { useState} from "react";
import { useHistory } from "react-router-dom";
import { addShoe } from "../../modules/shoeManager";
import { Form, FormGroup, Label, Input, Button } from "reactstrap";

const ShoeForm = () => {
    const history = useHistory();
    const emptyShoe = {
        name: "",
        brandId: "",
        releaseDate: "",
        retailPrice: "",
        purchaseDate: "",
        title: "",
        colorWay: "",
        collectionId: "",
    };

    const [shoe, setShoe] = useState(emptyShoe);
    const handleInputChange = (evt) => {
        const value = evt.target.value;
        const key = evt.target.id;

        const shoeCopy = {...shoe};

        shoeCopy[key] = value;
        setShoe(shoeCopy);
    };
    const handleSave = (evt) => {
        evt.preventDefault();
        addShoe(shoe).then((s) => {
            history.push("/shoe");
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
                placeholder="Shoe Name"
                value={shoe.name}
                onChange={handleInputChange}
                />
            </FormGroup>
            <FormGroup>
                <Label for="releaseDate">ReleaseDate</Label>
                <Input
                type="date"
                name="releaseDate"
                id="releaseDate"
                placeholder="ReleaseDate"
                value={shoe.releaseDate}
                onChange={handleInputChange}
                />
            </FormGroup>
            <FormGroup>
                <Label for="retailPrice">RetailPrice</Label>
                <Input
                type="number"
                name="retailPrice"
                id="retailPrice"
                placeholder="RetailPrice"
                value={shoe.retailPrice}
                onChange={handleInputChange}
                />
            </FormGroup>
            <FormGroup>
                <Label for="purchaseDate">ReleaseDate</Label>
                <Input
                type="date"
                name="purchaseDate"
                id="purchaseDate"
                placeholder="PurchaseDate"
                value={shoe.purchaseDate}
                onChange={handleInputChange}
                />
            </FormGroup>
            <FormGroup>
                <Label for="title">Title</Label>
                <Input
                type="text"
                name="title"
                id="title"
                placeholder="Shoe Title"
                value={shoe.title}
                onChange={handleInputChange}
                />
            </FormGroup>
            <FormGroup>
                <Label for="colorWay">ColorWay</Label>
                <Input
                type="text"
                name="colorWay"
                id="colorWay"
                placeholder="ColorWay"
                value={shoe.colorWay}
                onChange={handleInputChange}
                />
            </FormGroup>
            <Button className="btn btn-primary" onClick={handleSave}>
        Submit
      </Button>
        </Form>
    );
};
export default ShoeForm;