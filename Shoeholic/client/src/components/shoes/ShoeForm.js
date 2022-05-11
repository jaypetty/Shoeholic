import React, { useEffect, useState} from "react";
import { useHistory } from "react-router-dom";
import { addShoe, addShoeTags } from "../../modules/shoeManager";
import { getAllTags } from "../../modules/tagManager";
import { getAllBrands } from "../../modules/brandManager";
import { getUserCollectionByUserId } from "../../modules/collectionManager";
import { Form, FormGroup, Label, Input, Button, } from "reactstrap";

const ShoeForm = () => {
    const history = useHistory();
    const emptyShoe = {
        name: "",
        brandId: 0,
        releaseDate: "",
        retailPrice: "",
        purchaseDate: "",
        title: "",
        colorWay: "",
        collectionId: 0,
       
    };

    const [shoe, setShoe] = useState(emptyShoe);
    const [tags, setTags] = useState([]);
    const [brands, setBrands] = useState([])
    const [collections, setCollections] = useState([])
    const [choosentags, setchoosentags] = useState([])

    const getTags = () => {
        getAllTags().then((tags) => setTags(tags))
    };

    const getBrands = () => {
        getAllBrands().then((brand) => setBrands(brand))
    };

    const getCollections = () => {
        getUserCollectionByUserId().then(collection => setCollections(collection));
    }

    useEffect(() => {
        getTags(),
        getBrands(),
        getCollections()
    }, [])

    const handleInputChange = (evt) => {
        let value = evt.target.value;
        let key = evt.target.id;
        
        const shoeCopy = {...shoe};
        if (key === "brandId") {
            value = parseInt(value)
        }

        if (key === "collectionId") {
            value = parseInt(value)
        }

        shoeCopy[key] = value;
        setShoe(shoeCopy);
    };
    const handleSave = (evt) => {
        evt.preventDefault();
        const shoeCopy = {...shoe};
        shoeCopy.choosentags = choosentags
        if (shoeCopy.brandId === 0){
            window.alert("Please select a brand.")
        }
        if (shoeCopy.collectionId === 0){
            window.alert("Please select a collection.")
        }
      
        addShoe(shoeCopy).then((s) => (history.push("/myshoes"))
        );
    };

    const handleTagCheckbox = (evt) => {
        const choosentagscopy = [...choosentags]
        if(choosentagscopy.includes(parseInt(evt.target.value))){
            const indexposition = choosentagscopy.indexOf(parseInt(evt.target.value))
            choosentagscopy.splice(indexposition, 1)
        }
        else{
            choosentagscopy.push(parseInt(evt.target.value))
        }
       
        setchoosentags(choosentagscopy)
        

    }


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
                    <Label for="BrandId">Brand</Label>
                    <Input type="select" id="brandId" name="brandId" onChange={handleInputChange} required>
                        <option value="0">Select a Brand</option>
                        {brands.map(b => <option key={b.id} value={b.id}>{b.name}</option>)}
                    </Input>
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
                <Label for="purchaseDate">PurchaseDate</Label>
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
            <FormGroup>
                    <Label for="CollectionId">Collection</Label>
                    <Input type="select" id="collectionId" name="collectionId" onChange={handleInputChange} required>
                        <option value="0">Select a Collection</option>
                        {collections.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
                    </Input>
                </FormGroup>
            <div>
            Please select your tag(s) for your shoe:
            {tags.map((tag) => {
                return <div>
                            <input type="checkbox" value={tag.id} onChange={handleTagCheckbox} />
                            <label>
                            {tag.name}
                            </label>
                </div>
            })}
            </div>

            <Button className="btn btn-primary" onClick={handleSave}>
        Submit
      </Button>
        </Form>
    );
};
export default ShoeForm;