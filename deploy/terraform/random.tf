# Generate a random integer to create a globally unique name
resource "random_integer" "example" {
  min = 10000
  max = 99999
}
