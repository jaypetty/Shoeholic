import React, { useState, useEffect} from "react";
import { useHistory, useParams } from "react-router-dom";
import { editShoe, getShoeById } from "../../modules/shoeManager";
import { Container, Form, FormGroup, Label, Input, Button } from "reactstrap";

const ShoeEdit = () => {
    
    const {id} = useParams();
    const history = useHistory();
    const emptyShoe = {
        name: "",
        releaseDate: "",
        retailPrice: "",
        purchaseDate: "",
        title: "",
        colorWay: "", 
    };

    const [shoe, setShoe] = useState(emptyShoe);

    useEffect(() => {
        getShoeById(id).then(setShoe);
    }, [id]);

    const handleInputChange = (evt) => {
        const value = evt.target.value;
        const key = evt.target.id;

        const shoeCopy = {...shoe};

        shoeCopy[key] = value;
        setShoe(shoeCopy);
    };

    const handleSave = (evt) => {
        evt.preventDefault();
        editShoe(shoe).then(() => {
            history.push(`/myshoes/details/${shoe.id}`);
        });
    };
   
    return (
        <Container>
            <Form>
                <FormGroup>
                <Label for="name">Name</Label>
                <Input
                type="text"
                name="name"
                id="name"
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
                value={shoe.retailPrice}
                onChange={handleInputChange}
                />
            </FormGroup>
            <FormGroup>
                <Label for="purchaseDate">PurchaseDate</Label>
                <Input
                type="date"
                name="purchaseDate"
                id="purchaseDate"
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
                value={shoe.colorWay}
                onChange={handleInputChange}
                />
            </FormGroup>
            <Button className="btn btn-primary" onClick={handleSave}>
                Submit
            </Button>
            </Form>
        </Container>
    )
}

export default ShoeEdit