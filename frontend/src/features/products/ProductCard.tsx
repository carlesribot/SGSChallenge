import styled from "styled-components";

type Product = {
  id: number;
  name: string;
  description: string;
  category: string;
  price: number;
  stock: number;
  imageUrl: string;
  isActive: boolean;
};

type Props = {
  product: Product;
};

export const ProductCard: React.FC<Product> = ({ product }: Props) => {
  return (
    <CardContainer>
      <ProductImage src={product.imageUrl} alt={product.name} />
      <ProductName>{product.name}</ProductName>
      <ProductStatus>{product.description}</ProductStatus>
      <ProductDescription>Category: {product.category}</ProductDescription>
      <ProductDetails>Price: {product.price}€</ProductDetails>
      <ProductDetails>Stock: {product.stock}</ProductDetails>
      <ProductStatus>{product.isActive ? "Active" : "Inactive"}</ProductStatus>
    </CardContainer>
  );
};

const CardContainer = styled.div`
  border: 1px solid #ddd;
  border-radius: 8px;
  padding: 16px;
  margin: 16px;
  width: 200px;
`;

const ProductImage = styled.img`
  max-width: 100%;
  height: auto;
`;

const ProductName = styled.h3`
  margin-top: 8px;
  font-size: 16px;
`;

const ProductDescription = styled.h4`
  margin-top: 8px;
  font-size: 14px;
`;

const ProductDetails = styled.h5`
  margin-top: 8px;
  font-size: 14px;
`;

const ProductStatus = styled.p`
  margin-top: 8px;
  font-size: 14px;
  color: #888;
`;
