## Kullanılan Teknolojiler

.Net Core, Ocelot

**Devops :** Docker

**Database :** Postgresql, MongoDB

## Projeyi Çalıştır

Aşağıdaki araçlara ihtiyacınız olacak:

- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
- [.NET 6.0 veya üstü](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Docker Desktop](https://www.docker.com/get-started/)

#### Yükleme
1. Projeyi klonlayın

    ```sh
    git clone https://github.com/emreincekara/rea-taskapp.git
    ```
2. Proje dizinine gidin

    ```bash
    cd rea-taskapp
    ```
3. `docker-compose.yml` dosyasını içeren dizinde aşağıdaki komutu çalıştırın:

    ```bash
    docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
    ```
4. Docker'ın tüm microservice'leri oluşturmasını bekleyin.
5. Microservice'leri aşağıdaki linkler ile çalıştırabilirsiniz:
- Catalog API - http://host.docker.internal:8000/swagger/index.html
- Customer API - http://host.docker.internal:8001/swagger/index.html
- Order API - http://host.docker.internal:8002/swagger/index.html
- API Gateway - http://host.docker.internal:8010/catalog
- pgAdmin PostgreSQL - http://host.docker.internal:5050 - admin@domain.com : admin1234

## API Kullanımı

### Catalog API

#### Tüm öğeleri getir

```http
  GET /catalog
```
##### Yanıt
```json
{
  "id": "string",
  "name": "string",
  "imageUrl": "string",
  "price": 0,
  "categoryId": "string",
  "category": {
    "id": "string",
    "name": "string",
    "products": [
      "string"
    ]
  }
}
```

#### Öğeyi getir veya Sil

```http
  GET|DELETE /catalog/${id}
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `uuid` | **Gerekli**. |

##### Yanıt
```json
{
  "name": "iPhone 11 128 GB",
  "imageUrl": "",
  "price": 12199.01,
  "categoryId": "584bd799-19e1-49c6-b61e-b3718d61d84e",
  "category": {
    "name": "Telefon",
    "products": [],
    "id": "584bd799-19e1-49c6-b61e-b3718d61d84e"
  },
  "id": "82d9a1cb-8db1-46ec-b940-21767ce8a05f"
}
```

#### Öğe Ekle veya Güncelle

```http
  POST|PUT /catalog
```
##### İstek
```json
{
  "name": "iPhone 11 128 GB",
  "imageUrl": "",
  "price": 12199.01,
  "categoryId": "584bd799-19e1-49c6-b61e-b3718d61d84e"
}
```

#### Ürün adına göre öğeyi getir

```http
  GET /catalog/getProductByName/${name}
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `name`      | `string` | **Gerekli**. Ürün adı. |

#### Kategori adına göre öğeyi getir

```http
  GET /catalog/getProductByCategory/${categoryName}
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `categoryName`      | `string` | **Gerekli**. Kategori adı. |

### Customer API

#### Tüm öğeleri getir

```http
  GET /customer
```
##### Yanıt
```json
{
  "id": "uuid",
  "name": "string",
  "email": "user@example.com",
  "createdAt": "timestamp",
  "updatedAt": "timestamp",
  "address": {
    "id": "uuid",
    "customerId": "uuid",
    "addressLine": "string",
    "city": "string",
    "country": "string",
    "cityCode": 0
  }
}
```

#### Öğeyi getir veya Sil

```http
  GET|DELETE /customer/${id}
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `uuid` | **Gerekli**. |

#### Öğe Ekle veya Güncelle

```http
  POST|PUT /customer
```
##### İstek
```json
{
  "id": "uuid",
  "name": "string",
  "email": "user@example.com",
  "createdAt": "timestamp",
  "updatedAt": "timestamp",
  "address": {
    "id": "uuid",
    "customerId": "uuid",
    "addressLine": "string",
    "city": "string",
    "country": "string",
    "cityCode": 0
  }
}
```

#### Doğrulama Kontrolü

```http
  GET /customer/validate/${id}
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `uuid` | **Gerekli**. |

##### Yanıt
```json
Boolean: true | false
```
### Order API

#### Tüm öğeleri getir

```http
  GET /order
```
##### Yanıt
```json
{
  "id": "uuid",
  "customerId": "uuid",
  "quantity": 0,
  "price": 0,
  "status": "string",
  "addressId": "uuid",
  "productId": "uuid",
  "createdAt": "timestamp",
  "updatedAt": "timestamp"
}
```

#### Öğeyi getir veya Sil

```http
  GET|DELETE /order/${id}
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `uuid` | **Gerekli**. |

#### Öğe Ekle veya Güncelle

```http
  POST|PUT /order
```
##### İstek
```json
{
  "id": "uuid",
  "customerId": "uuid",
  "quantity": 0,
  "price": 0,
  "status": "string",
  "addressId": "uuid",
  "productId": "uuid",
  "createdAt": "timestamp",
  "updatedAt": "timestamp"
}
```

#### CustomerId'ye göre öğeyi getir

```http
  GET /order/getOrdersByCustomerId/${customerId}
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `customerId`      | `uuid` | **Gerekli**. |

#### Id'ye göre Status değerini güncelle

```http
  GET /order/${id}/${status}
```

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `uuid` | **Gerekli**. |
| `status`      | `string` | **Gerekli**. |

## Lisans

[MIT](https://choosealicense.com/licenses/mit/)
